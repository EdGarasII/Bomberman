using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using BombermanGame.Entities;
using BombermanGame.Factories;

namespace BombermanGame.Network
{
    // Game client for connecting to multiplayer server
    public class GameClient
    {
        private TcpClient client;
        private NetworkStream stream;
        private string serverAddress;
        private int serverPort;
        private string playerId;
        private string playerName;
        private bool isConnected;
        
        // Game state from server
        public Dictionary<string, Player> RemotePlayers { get; private set; }
        public List<Bomb> Bombs { get; private set; }
        public List<Explosion> Explosions { get; private set; }
        public Tile[,] Board { get; private set; }
        
        private AbstractEntityFactory entityFactory;
        
        public event Action<GameStateMessage> OnGameStateReceived;
        public event Action<string> OnPlayerJoined;
        public event Action<string> OnPlayerLeft;
        public event Action OnConnected;
        public event Action OnDisconnected;
        public event Action<string> OnError;
        
        public GameClient()
        {
            RemotePlayers = new Dictionary<string, Player>();
            Bombs = new List<Bomb>();
            Explosions = new List<Explosion>();
            entityFactory = new StandardEntityFactory();
            isConnected = false;
        }
        
        public async Task<bool> ConnectAsync(string address, int port, string name)
        {
            try
            {
                serverAddress = address;
                serverPort = port;
                playerName = name;
                
                client = new TcpClient();
                await client.ConnectAsync(address, port);
                stream = client.GetStream();
                
                isConnected = true;
                
                // Start receiving messages first
                Task.Run(ReceiveMessages);
                
                // Give a tiny delay for the receive task to start
                await Task.Delay(50);
                
                // Send join game message
                var joinMsg = new JoinGameMessage
                {
                    PlayerName = name ?? "Unknown",
                    PlayerId = "",
                    Type = MessageType.JoinGame
                };
                
                Console.WriteLine($"Client sending JoinGame message: PlayerName='{joinMsg.PlayerName}'");
                var testJson = System.Text.Json.JsonSerializer.Serialize(joinMsg);
                Console.WriteLine($"Test serialization: {testJson}");
                SendMessage(joinMsg);
                Console.WriteLine("JoinGame message sent");
                
                // Note: OnConnected will be called when we receive PlayerJoined message from server
                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Failed to connect: {ex.Message}");
                isConnected = false;
                return false;
            }
        }
        
        public void Disconnect()
        {
            if (isConnected)
            {
                var msg = new NetworkMessage
                {
                    Type = MessageType.Disconnect,
                    PlayerId = playerId
                };
                SendMessage(msg);
            }
            
            isConnected = false;
            stream?.Close();
            client?.Close();
            OnDisconnected?.Invoke();
        }
        
        public void SendMove(int deltaX, int deltaY)
        {
            if (!isConnected) return;
            
            var msg = new PlayerMoveMessage
            {
                PlayerId = playerId,
                DeltaX = deltaX,
                DeltaY = deltaY
            };
            
            SendMessage(msg);
        }
        
        public void SendPlaceBomb()
        {
            if (!isConnected) return;
            
            var msg = new PlaceBombMessage
            {
                PlayerId = playerId
            };
            
            SendMessage(msg);
        }
        
        private void SendMessage(NetworkMessage message)
        {
            try
            {
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
                    WriteIndented = false,
                    IncludeFields = false,
                    PropertyNamingPolicy = null // Use property names as-is
                };
                
                // Serialize with the actual runtime type
                string json;
                if (message is JoinGameMessage joinMsg)
                {
                    json = System.Text.Json.JsonSerializer.Serialize(joinMsg, options);
                }
                else
                {
                    json = System.Text.Json.JsonSerializer.Serialize(message, message.GetType(), options);
                }
                
                Console.WriteLine($"Client serialized message (type: {message.GetType().Name}): {json}");
                var data = Encoding.UTF8.GetBytes(json + "\n");
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Failed to send message: {ex.Message}");
                Console.WriteLine($"SendMessage error: {ex}");
                Disconnect();
            }
        }
        
        private async Task ReceiveMessages()
        {
            byte[] buffer = new byte[4096];
            StringBuilder messageBuffer = new StringBuilder();
            
            try
            {
                while (isConnected && client.Connected)
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
                if (isConnected)
                {
                    OnError?.Invoke($"Connection error: {ex.Message}");
                }
            }
            finally
            {
                Disconnect();
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
                    Console.WriteLine("Failed to deserialize base message");
                    return;
                }
                
                switch (baseMsg.Type)
                {
                    case MessageType.PlayerJoined:
                        Console.WriteLine($"Client received PlayerJoined: {json}");
                        var options = new System.Text.Json.JsonSerializerOptions
                        {
                            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
                            PropertyNameCaseInsensitive = true
                        };
                        var joinMsg = System.Text.Json.JsonSerializer.Deserialize<PlayerJoinedMessage>(json, options);
                        if (joinMsg != null)
                        {
                            bool wasFirstConnection = string.IsNullOrEmpty(playerId);
                            Console.WriteLine($"Processing PlayerJoined - wasFirstConnection: {wasFirstConnection}, AssignedId: '{joinMsg.AssignedPlayerId ?? "NULL"}'");
                            
                            if (wasFirstConnection && !string.IsNullOrEmpty(joinMsg.AssignedPlayerId))
                            {
                                playerId = joinMsg.AssignedPlayerId;
                                Console.WriteLine($"Client connected, assigned ID: {playerId}");
                                // First time we get this message, we're fully connected
                                OnConnected?.Invoke();
                            }
                            OnPlayerJoined?.Invoke(joinMsg.PlayerName ?? "");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to deserialize PlayerJoined message: {json}");
                        }
                        break;
                        
                    case MessageType.PlayerLeft:
                        OnPlayerLeft?.Invoke(baseMsg.PlayerId ?? "");
                        if (!string.IsNullOrEmpty(baseMsg.PlayerId) && RemotePlayers.ContainsKey(baseMsg.PlayerId))
                        {
                            RemotePlayers.Remove(baseMsg.PlayerId);
                        }
                        break;
                        
                    case MessageType.GameStateUpdate:
                        Console.WriteLine($"Client received GameStateUpdate: {json.Substring(0, Math.Min(200, json.Length))}...");
                        var stateOptions = new System.Text.Json.JsonSerializerOptions
                        {
                            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
                            PropertyNameCaseInsensitive = true
                        };
                        var stateMsg = System.Text.Json.JsonSerializer.Deserialize<GameStateMessage>(json, stateOptions);
                        if (stateMsg != null)
                        {
                            Console.WriteLine($"Deserialized GameStateUpdate with {stateMsg.Players.Count} players");
                            UpdateGameState(stateMsg);
                            OnGameStateReceived?.Invoke(stateMsg);
                        }
                        else
                        {
                            Console.WriteLine($"Failed to deserialize GameStateUpdate");
                        }
                        break;
                        
                    case MessageType.GameStart:
                        // Game started
                        break;
                        
                    case MessageType.Error:
                        OnError?.Invoke("Server error occurred");
                        break;
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Error processing message: {ex.GetType().Name}: {ex.Message}");
                Console.WriteLine($"Exception details: {ex}");
                Console.WriteLine($"JSON was: {json}");
            }
        }
        
        private void UpdateGameState(GameStateMessage stateMsg)
        {
            if (stateMsg == null) return;
            
            // Update remote players
            RemotePlayers.Clear();
            foreach (var playerData in stateMsg.Players)
            {
                if (playerData != null && !string.IsNullOrEmpty(playerData.PlayerId) && playerData.PlayerId != playerId) // Don't add self
                {
                    if (!RemotePlayers.ContainsKey(playerData.PlayerId))
                    {
                        var newPlayer = EntityFactory.CreatePlayer(playerData.X, playerData.Y);
                        RemotePlayers[playerData.PlayerId] = newPlayer;
                    }
                    
                    var remotePlayer = RemotePlayers[playerData.PlayerId];
                    remotePlayer.X = playerData.X;
                    remotePlayer.Y = playerData.Y;
                    remotePlayer.BombCount = playerData.BombCount;
                    remotePlayer.MaxBombs = playerData.MaxBombs;
                    remotePlayer.BombRange = playerData.BombRange;
                    remotePlayer.IsActive = playerData.IsAlive;
                }
            }
            
            // Update bombs
            Bombs.Clear();
            foreach (var bombData in stateMsg.Bombs)
            {
                var bomb = entityFactory.CreateBomb(bombData.X, bombData.Y, bombData.Range);
                bomb.Timer = bombData.Timer;
                Bombs.Add(bomb);
            }
            
            // Update explosions
            Explosions.Clear();
            foreach (var expData in stateMsg.Explosions)
            {
                var explosion = entityFactory.CreateExplosion(expData.X, expData.Y, 30 - expData.Duration);
                Explosions.Add(explosion);
            }
        }
        
        public string GetPlayerId() => playerId;
        public bool IsConnected() => isConnected;
    }
}

