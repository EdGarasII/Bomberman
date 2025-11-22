using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Timers;
using BombermanGame.Entities;
using BombermanGame.Factories;

namespace BombermanGame.Network
{
    // Game server for hosting multiplayer matches
    public class GameServer
    {
        private TcpListener listener;
        private Dictionary<string, ClientConnection> clients;
        private Dictionary<string, Player> players;
        private List<Bomb> bombs;
        private List<Explosion> explosions;
        private Tile[,] board;
        private AbstractEntityFactory entityFactory;
        private bool isRunning;
        private const int PORT = 8888;
        private const int BOARD_WIDTH = 20;
        private const int BOARD_HEIGHT = 15;
        private const int TILE_SIZE = 32;
        
        private System.Timers.Timer gameTimer;
        private Random random;
        
        public event Action<string> OnPlayerJoined;
        public event Action<string> OnPlayerLeft;
        public event Action OnGameStateUpdated;
        
        public GameServer()
        {
            clients = new Dictionary<string, ClientConnection>();
            players = new Dictionary<string, Player>();
            bombs = new List<Bomb>();
            explosions = new List<Explosion>();
            entityFactory = new StandardEntityFactory();
            random = new Random();
            
            gameTimer = new System.Timers.Timer(16); // ~60 FPS
            gameTimer.Elapsed += (sender, e) => UpdateGame(sender, e);
            gameTimer.AutoReset = true;
        }
        
        public void Start()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, PORT);
                listener.Start();
                isRunning = true;
                
                Console.WriteLine($"Server started on port {PORT}");
                
                GenerateLevel();
                
                // Start game timer
                gameTimer.Start();
                
                // Accept clients asynchronously
                Task.Run(AcceptClients);
                
                Console.WriteLine($"Server game timer started, broadcasting at ~60 FPS");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting server: {ex.Message}");
                throw;
            }
        }
        
        public void Stop()
        {
            isRunning = false;
            gameTimer.Stop();
            
            foreach (var client in clients.Values)
            {
                client.Close();
            }
            
            listener?.Stop();
            clients.Clear();
            players.Clear();
        }
        
        private async Task AcceptClients()
        {
            while (isRunning)
            {
                try
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    string clientId = Guid.NewGuid().ToString();
                    var connection = new ClientConnection(clientId, client, this);
                    clients[clientId] = connection;
                    
                    Console.WriteLine($"Client connected: {clientId}");
                    Task.Run(() => connection.HandleClient());
                }
                catch (Exception ex)
                {
                    if (isRunning)
                        Console.WriteLine($"Error accepting client: {ex.Message}");
                }
            }
        }
        
        private void GenerateLevel()
        {
            board = new Tile[BOARD_WIDTH, BOARD_HEIGHT];
            
            // Fill with empty tiles
            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                for (int y = 0; y < BOARD_HEIGHT; y++)
                {
                    board[x, y] = entityFactory.CreateTile(x, y, TileType.Empty);
                }
            }
            
            // Add border walls
            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                board[x, 0] = entityFactory.CreateTile(x, 0, TileType.Wall);
                board[x, BOARD_HEIGHT - 1] = entityFactory.CreateTile(x, BOARD_HEIGHT - 1, TileType.Wall);
            }
            for (int y = 0; y < BOARD_HEIGHT; y++)
            {
                board[0, y] = entityFactory.CreateTile(0, y, TileType.Wall);
                board[BOARD_WIDTH - 1, y] = entityFactory.CreateTile(BOARD_WIDTH - 1, y, TileType.Wall);
            }
            
            // Add some breakable walls
            for (int x = 2; x < BOARD_WIDTH - 2; x += 2)
            {
                for (int y = 2; y < BOARD_HEIGHT - 2; y += 2)
                {
                    if (random.NextDouble() < 0.7)
                    {
                        board[x, y] = entityFactory.CreateTile(x, y, TileType.BreakableWall);
                    }
                }
            }
        }
        
        private void UpdateGame(object sender, ElapsedEventArgs e)
        {
            if (players.Count == 0) return; // No players yet, don't broadcast
            
            // Update entities
            foreach (var player in players.Values)
            {
                player.Update();
            }
            
            UpdateBombs();
            UpdateExplosions();
            
            // Broadcast game state to all clients
            BroadcastGameState();
        }
        
        private void UpdateBombs()
        {
            for (int i = bombs.Count - 1; i >= 0; i--)
            {
                bombs[i].Update();
                if (bombs[i].ShouldExplode())
                {
                    ExplodeBomb(bombs[i]);
                    bombs.RemoveAt(i);
                }
            }
        }
        
        private void UpdateExplosions()
        {
            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                explosions[i].Update();
                if (explosions[i].IsFinished())
                {
                    explosions.RemoveAt(i);
                }
            }
        }
        
        private void ExplodeBomb(Bomb bomb)
        {
            int centerX = bomb.X / TILE_SIZE;
            int centerY = bomb.Y / TILE_SIZE;
            
            explosions.Add(entityFactory.CreateExplosion(centerX * TILE_SIZE, centerY * TILE_SIZE, 0));
            
            for (int direction = 0; direction < 4; direction++)
            {
                for (int distance = 1; distance <= bomb.Range; distance++)
                {
                    int x = centerX;
                    int y = centerY;
                    
                    switch (direction)
                    {
                        case 0: x += distance; break;
                        case 1: x -= distance; break;
                        case 2: y += distance; break;
                        case 3: y -= distance; break;
                    }
                    
                    if (x < 0 || x >= BOARD_WIDTH || y < 0 || y >= BOARD_HEIGHT)
                        break;
                    
                    if (board[x, y].Type == TileType.Wall)
                        break;
                    
                    if (board[x, y].IsBreakable())
                    {
                        board[x, y].Break();
                        break;
                    }
                    
                    explosions.Add(entityFactory.CreateExplosion(x * TILE_SIZE, y * TILE_SIZE, distance));
                }
            }
            
            // Check for player deaths
            foreach (var player in players.Values)
            {
                if (player.IsActive && CheckExplosionCollision(player, bomb.X, bomb.Y, bomb.Range))
                {
                    player.IsActive = false;
                }
            }
        }
        
        private bool CheckExplosionCollision(Player player, int bombX, int bombY, int range)
        {
            int playerTileX = player.X / TILE_SIZE;
            int playerTileY = player.Y / TILE_SIZE;
            int bombTileX = bombX / TILE_SIZE;
            int bombTileY = bombY / TILE_SIZE;
            
            // Check if player is in explosion range
            for (int direction = 0; direction < 4; direction++)
            {
                for (int distance = 0; distance <= range; distance++)
                {
                    int x = bombTileX;
                    int y = bombTileY;
                    
                    switch (direction)
                    {
                        case 0: x += distance; break;
                        case 1: x -= distance; break;
                        case 2: y += distance; break;
                        case 3: y -= distance; break;
                    }
                    
                    if (x == playerTileX && y == playerTileY)
                        return true;
                }
            }
            
            return false;
        }
        
        public void HandleJoinGame(string clientId, string playerName)
        {
            // Create player at different starting positions
            // Use players.Count BEFORE adding to get the correct index
            int spawnIndex = players.Count;
            int startX = (spawnIndex % 4) switch
            {
                0 => 1 * TILE_SIZE,                    // Top-left corner
                1 => (BOARD_WIDTH - 2) * TILE_SIZE,    // Top-right corner
                2 => 1 * TILE_SIZE,                    // Bottom-left corner
                3 => (BOARD_WIDTH - 2) * TILE_SIZE,    // Bottom-right corner
                _ => 1 * TILE_SIZE
            };
            
            int startY = (spawnIndex % 4) switch
            {
                0 => 1 * TILE_SIZE,                    // Top-left corner
                1 => 1 * TILE_SIZE,                    // Top-right corner
                2 => (BOARD_HEIGHT - 2) * TILE_SIZE,   // Bottom-left corner
                3 => (BOARD_HEIGHT - 2) * TILE_SIZE,   // Bottom-right corner
                _ => 1 * TILE_SIZE
            };
            
            Console.WriteLine($"Creating player {clientId} at spawn index {spawnIndex}, position ({startX}, {startY})");
            var player = EntityFactory.CreatePlayer(startX, startY);
            players[clientId] = player;
            
            // Send player joined confirmation
            var joinMsg = new PlayerJoinedMessage
            {
                PlayerId = clientId,
                PlayerName = playerName ?? "",
                AssignedPlayerId = clientId
            };
            
            Console.WriteLine($"Sending PlayerJoined to {clientId}");
            SendToClient(clientId, joinMsg);
            Console.WriteLine($"PlayerJoined sent to {clientId}");
            
            // Broadcast to all clients (except the one who just joined, they already got the message)
            var broadcastMsg = new PlayerJoinedMessage
            {
                PlayerId = clientId,
                PlayerName = playerName,
                AssignedPlayerId = clientId
            };
            // Note: We broadcast to all, but the joining client gets it twice - that's okay
            BroadcastToAll(broadcastMsg);
            
            // Immediately send game state to the new player so they see their spawn position
            Console.WriteLine($"Sending initial game state to new player {clientId} at ({startX}, {startY})");
            BroadcastGameState();
            
            OnPlayerJoined?.Invoke(playerName);
        }
        
        public void HandlePlayerMove(string clientId, int deltaX, int deltaY)
        {
            if (!players.ContainsKey(clientId))
            {
                Console.WriteLine($"HandlePlayerMove: Player {clientId} not found in players dictionary");
                return;
            }
            
            var player = players[clientId];
            if (!player.IsActive)
            {
                Console.WriteLine($"HandlePlayerMove: Player {clientId} is not active");
                return;
            }
            
            int oldX = player.X;
            int oldY = player.Y;
            int newX = player.X + deltaX;
            int newY = player.Y + deltaY;
            
            Console.WriteLine($"HandlePlayerMove: {clientId} trying to move from ({oldX}, {oldY}) to ({newX}, {newY})");
            
            // Validate movement
            if (IsValidPosition(newX, newY, player.Size))
            {
                player.X = newX;
                player.Y = newY;
                Console.WriteLine($"HandlePlayerMove: {clientId} moved to ({newX}, {newY})");
            }
            else
            {
                Console.WriteLine($"HandlePlayerMove: {clientId} movement blocked - invalid position");
            }
        }
        
        public void HandlePlaceBomb(string clientId)
        {
            if (!players.ContainsKey(clientId)) return;
            
            var player = players[clientId];
            if (!player.IsActive || player.BombCount <= 0) return;
            
            int bombX = (player.X + player.Size / 2) / TILE_SIZE * TILE_SIZE;
            int bombY = (player.Y + player.Size / 2) / TILE_SIZE * TILE_SIZE;
            
            // Check if position is already occupied
            bool occupied = bombs.Any(b => b.X == bombX && b.Y == bombY);
            
            if (!occupied)
            {
                bombs.Add(entityFactory.CreateBomb(bombX, bombY, player.BombRange));
                player.PlaceBomb();
            }
        }
        
        private bool IsValidPosition(int x, int y, int size)
        {
            if (x < 0 || y < 0 || x + size >= BOARD_WIDTH * TILE_SIZE || y + size >= BOARD_HEIGHT * TILE_SIZE)
                return false;
            
            int tileX = x / TILE_SIZE;
            int tileY = y / TILE_SIZE;
            int tileX2 = (x + size - 1) / TILE_SIZE;
            int tileY2 = (y + size - 1) / TILE_SIZE;
            
            if (tileX >= BOARD_WIDTH || tileY >= BOARD_HEIGHT || tileX2 >= BOARD_WIDTH || tileY2 >= BOARD_HEIGHT)
                return false;
            
            if (board[tileX, tileY].IsWall() ||
                board[tileX2, tileY].IsWall() ||
                board[tileX, tileY2].IsWall() ||
                board[tileX2, tileY2].IsWall())
            {
                return false;
            }
            
            return true;
        }
        
        public void HandleDisconnect(string clientId)
        {
            clients.Remove(clientId);
            players.Remove(clientId);
            
            BroadcastToAll(new NetworkMessage
            {
                Type = MessageType.PlayerLeft,
                PlayerId = clientId
            });
            
            OnPlayerLeft?.Invoke(clientId);
        }
        
        private void BroadcastGameState()
        {
            Console.WriteLine($"Broadcasting game state to {clients.Count} clients with {players.Count} players");
            foreach (var p in players)
            {
                Console.WriteLine($"  Player {p.Key}: ({p.Value.X}, {p.Value.Y}), Active: {p.Value.IsActive}");
            }
            
            var stateMsg = new GameStateMessage
            {
                Players = players.Select(p => new PlayerData
                {
                    PlayerId = p.Key,
                    X = p.Value.X,
                    Y = p.Value.Y,
                    Speed = p.Value.Speed,
                    BombCount = p.Value.BombCount,
                    MaxBombs = p.Value.MaxBombs,
                    BombRange = p.Value.BombRange,
                    IsAlive = p.Value.IsActive
                }).ToList(),
                
                Bombs = bombs.Select(b => new BombData
                {
                    X = b.X,
                    Y = b.Y,
                    Timer = b.Timer,
                    Range = b.Range
                }).ToList(),
                
                Explosions = explosions.Select(e => new ExplosionData
                {
                    X = e.X,
                    Y = e.Y,
                    Duration = Explosion.EXPLOSION_DURATION - e.Timer
                }).ToList()
            };
            
            BroadcastToAll(stateMsg);
            OnGameStateUpdated?.Invoke();
        }
        
        private void BroadcastToAll(NetworkMessage message)
        {
            var options = new System.Text.Json.JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
                PropertyNamingPolicy = null
            };
            
            // Serialize with the actual runtime type
            string json = System.Text.Json.JsonSerializer.Serialize(message, message.GetType(), options);
            var data = Encoding.UTF8.GetBytes(json + "\n");
            
            List<string> disconnectedClients = new List<string>();
            
            foreach (var client in clients.Values)
            {
                try
                {
                    client.Send(data);
                }
                catch
                {
                    disconnectedClients.Add(client.ClientId);
                }
            }
            
            foreach (var id in disconnectedClients)
            {
                HandleDisconnect(id);
            }
        }
        
        private void SendToClient(string clientId, NetworkMessage message)
        {
            if (!clients.ContainsKey(clientId)) return;
            
            try
            {
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
                    PropertyNamingPolicy = null
                };
                
                // Serialize with the actual runtime type
                string json;
                if (message is PlayerJoinedMessage joinedMsg)
                {
                    json = System.Text.Json.JsonSerializer.Serialize(joinedMsg, options);
                }
                else
                {
                    json = System.Text.Json.JsonSerializer.Serialize(message, message.GetType(), options);
                }
                
                Console.WriteLine($"Server sending {message.GetType().Name} to {clientId}: {json}");
                var data = Encoding.UTF8.GetBytes(json + "\n");
                clients[clientId].Send(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending to client {clientId}: {ex.Message}");
                HandleDisconnect(clientId);
            }
        }
        
        // Inner class for client connections
        private class ClientConnection
        {
            public string ClientId { get; }
            private TcpClient client;
            private NetworkStream stream;
            private GameServer server;
            
            public ClientConnection(string clientId, TcpClient client, GameServer server)
            {
                ClientId = clientId;
                this.client = client;
                this.server = server;
                stream = client.GetStream();
            }
            
            public async Task HandleClient()
            {
                byte[] buffer = new byte[4096];
                StringBuilder messageBuffer = new StringBuilder();
                
                try
                {
                    while (client.Connected)
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0) break;
                        
                        string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        messageBuffer.Append(data);
                        
                        // Process complete messages (newline separated)
                        string[] messages = messageBuffer.ToString().Split('\n');
                        messageBuffer.Clear();
                        
                        if (messages.Length > 1)
                        {
                            for (int i = 0; i < messages.Length - 1; i++)
                            {
                                if (!string.IsNullOrWhiteSpace(messages[i]))
                                {
                                    ProcessMessage(messages[i]);
                                }
                            }
                            messageBuffer.Append(messages[messages.Length - 1]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Client {ClientId} error: {ex.Message}");
                }
                finally
                {
                    server.HandleDisconnect(ClientId);
                    Close();
                }
            }
            
            private void ProcessMessage(string json)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(json))
                        return;
                    
                    var baseMsg = System.Text.Json.JsonSerializer.Deserialize<NetworkMessage>(json);
                    if (baseMsg == null)
                    {
                        Console.WriteLine($"Failed to deserialize base message from {ClientId}");
                        return;
                    }
                    
                    switch (baseMsg.Type)
                    {
                        case MessageType.JoinGame:
                            Console.WriteLine($"Server received JoinGame JSON: {json}");
                            var options = new System.Text.Json.JsonSerializerOptions
                            {
                                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
                                PropertyNameCaseInsensitive = true
                            };
                            var joinMsg = System.Text.Json.JsonSerializer.Deserialize<JoinGameMessage>(json, options);
                            if (joinMsg != null)
                            {
                                Console.WriteLine($"Deserialized JoinGame - PlayerName: '{joinMsg.PlayerName ?? "NULL"}', PlayerId: '{joinMsg.PlayerId ?? "NULL"}'");
                                if (!string.IsNullOrEmpty(joinMsg.PlayerName))
                                {
                                    Console.WriteLine($"Processing JoinGame from {ClientId}: {joinMsg.PlayerName}");
                                    server.HandleJoinGame(ClientId, joinMsg.PlayerName);
                                    Console.WriteLine($"JoinGame processed, PlayerJoined message should have been sent to {ClientId}");
                                }
                                else
                                {
                                    Console.WriteLine($"JoinGame message has empty PlayerName, using ClientId as fallback");
                                    server.HandleJoinGame(ClientId, $"Player_{ClientId.Substring(0, 8)}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Failed to deserialize JoinGame message: {json}");
                            }
                            break;
                            
                        case MessageType.PlayerMove:
                            Console.WriteLine($"Server received PlayerMove from {ClientId}: {json}");
                            var moveOptions = new System.Text.Json.JsonSerializerOptions
                            {
                                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
                                PropertyNameCaseInsensitive = true
                            };
                            var moveMsg = System.Text.Json.JsonSerializer.Deserialize<PlayerMoveMessage>(json, moveOptions);
                            if (moveMsg != null)
                            {
                                Console.WriteLine($"Deserialized PlayerMove: DeltaX={moveMsg.DeltaX}, DeltaY={moveMsg.DeltaY}");
                                server.HandlePlayerMove(ClientId, moveMsg.DeltaX, moveMsg.DeltaY);
                            }
                            else
                            {
                                Console.WriteLine($"Failed to deserialize PlayerMove message");
                            }
                            break;
                            
                        case MessageType.PlaceBomb:
                            server.HandlePlaceBomb(ClientId);
                            break;
                            
                        case MessageType.Disconnect:
                            server.HandleDisconnect(ClientId);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message from {ClientId}: {ex.GetType().Name}: {ex.Message}");
                    Console.WriteLine($"JSON was: {json}");
                }
            }
            
            public void Send(byte[] data)
            {
                try
                {
                    stream.Write(data, 0, data.Length);
                }
                catch
                {
                    // Connection lost
                }
            }
            
            public void Close()
            {
                try
                {
                    stream?.Close();
                    client?.Close();
                }
                catch { }
            }
        }
        
        public int GetPlayerCount() => players.Count;
        public bool IsRunning() => isRunning;
    }
}

