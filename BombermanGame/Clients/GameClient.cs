using System;
using System.Drawing;
using BombermanGame.Facades;
using BombermanGame.Core;

namespace BombermanGame.Clients
{
    // FACADE PATTERN - Client 1: Main game client
    // This client uses GameFacade to simplify interaction with complex game subsystems
    public class MainGameClient
    {
        private GameFacade gameFacade;
        private int boardWidth;
        private int boardHeight;
        
        public MainGameClient(int width, int height)
        {
            boardWidth = width;
            boardHeight = height;
            gameFacade = new GameFacade(width, height);
        }
        
        public void StartGame()
        {
            Console.WriteLine("MainGameClient: Starting new game...");
            
            // Instead of manually coordinating:
            // - PlayerManager
            // - LevelManager
            // - BombManager
            // - PowerUpManager
            // - RenderingManager
            // - CommandInvoker
            // - GameEventSystem
            
            // We simply use the Facade:
            gameFacade.InitializeGame();
            
            Console.WriteLine("MainGameClient: Game initialized through facade.");
        }
        
        public void HandlePlayerMovement(int deltaX, int deltaY)
        {
            // Client doesn't need to know about:
            // - Command pattern implementation
            // - Collision detection
            // - Tile system
            // - Command history
            
            // Just use the facade:
            gameFacade.MovePlayer(deltaX, deltaY);
        }
        
        public void HandlePlayerAction()
        {
            // Client doesn't need to know about:
            // - Bomb placement logic
            // - Grid alignment
            // - Player bomb count management
            // - Bomb list management
            
            // Just use the facade:
            gameFacade.PlaceBomb();
        }
        
        public void UpdateGameState()
        {
            // Client doesn't need to coordinate:
            // - Player updates
            // - Bomb timers
            // - Explosion animations
            // - Enemy AI
            // - Collision detection
            // - Event notifications
            
            // Facade handles it all:
            gameFacade.UpdateGame();
        }
        
        public void RenderGame(Graphics g)
        {
            // Client doesn't need to manage:
            // - Rendering order (tiles, then entities, then effects)
            // - Accessing multiple entity lists
            // - Tile board rendering
            // - Different rendering strategies
            
            var player = gameFacade.GetPlayer();
            var bombs = gameFacade.GetBombs();
            var explosions = gameFacade.GetExplosions();
            var enemies = gameFacade.GetEnemies();
            var board = gameFacade.GetBoard();
            
            // Simple rendering using facade-provided data
            Console.WriteLine($"MainGameClient: Rendering {bombs.Count} bombs, {explosions.Count} explosions");
        }
    }
    
    // FACADE PATTERN - Client 2: AI testing client
    // Different client with different needs, but still uses the same facade
    public class AITestingClient
    {
        private GameFacade gameFacade;
        private Random random;
        
        public AITestingClient(int width, int height)
        {
            gameFacade = new GameFacade(width, height);
            random = new Random();
        }
        
        public void RunAISimulation(int steps)
        {
            Console.WriteLine($"AITestingClient: Running AI simulation for {steps} steps...");
            
            // Initialize through facade
            gameFacade.InitializeGame();
            
            for (int i = 0; i < steps; i++)
            {
                // Simulate random AI movements
                int direction = random.Next(4);
                int deltaX = 0, deltaY = 0;
                
                switch (direction)
                {
                    case 0: deltaX = 5; break;  // Right
                    case 1: deltaX = -5; break; // Left
                    case 2: deltaY = 5; break;  // Down
                    case 3: deltaY = -5; break; // Up
                }
                
                // Use facade to move player
                gameFacade.MovePlayer(deltaX, deltaY);
                
                // Randomly place bombs
                if (random.Next(100) < 10) // 10% chance
                {
                    gameFacade.PlaceBomb();
                }
                
                // Update game state
                gameFacade.UpdateGame();
                
                // Collect statistics
                if (i % 100 == 0)
                {
                    var player = gameFacade.GetPlayer();
                    var bombs = gameFacade.GetBombs();
                    Console.WriteLine($"  Step {i}: Player at ({player?.X}, {player?.Y}), Active bombs: {bombs.Count}");
                }
            }
            
            Console.WriteLine("AITestingClient: Simulation complete.");
        }
        
        public void StressTestBombSystem(int bombCount)
        {
            Console.WriteLine($"AITestingClient: Stress testing with {bombCount} bombs...");
            
            gameFacade.InitializeGame();
            
            // Place multiple bombs rapidly
            for (int i = 0; i < bombCount; i++)
            {
                gameFacade.PlaceBomb();
                
                // Update a few times
                for (int j = 0; j < 5; j++)
                {
                    gameFacade.UpdateGame();
                }
            }
            
            var activeBombs = gameFacade.GetBombs();
            var activeExplosions = gameFacade.GetExplosions();
            
            Console.WriteLine($"AITestingClient: Test complete. Active bombs: {activeBombs.Count}, Explosions: {activeExplosions.Count}");
        }
    }
    
    // FACADE PATTERN - Client 3: Replay/Recording client
    // Yet another client with different requirements
    public class ReplayClient
    {
        private GameFacade gameFacade;
        private System.Collections.Generic.List<string> actionLog;
        
        public ReplayClient(int width, int height)
        {
            gameFacade = new GameFacade(width, height);
            actionLog = new System.Collections.Generic.List<string>();
        }
        
        public void RecordGameSession()
        {
            Console.WriteLine("ReplayClient: Recording game session...");
            
            gameFacade.InitializeGame();
            LogAction("Game Initialized");
            
            // The facade simplifies recording because we don't need to track
            // all the individual subsystems (PlayerManager, BombManager, etc.)
        }
        
        public void RecordPlayerMove(int deltaX, int deltaY)
        {
            gameFacade.MovePlayer(deltaX, deltaY);
            LogAction($"Move: ({deltaX}, {deltaY})");
        }
        
        public void RecordBombPlacement()
        {
            var player = gameFacade.GetPlayer();
            if (player != null)
            {
                gameFacade.PlaceBomb();
                LogAction($"Bomb placed at ({player.X}, {player.Y})");
            }
        }
        
        public void RecordFrame()
        {
            gameFacade.UpdateGame();
            
            var player = gameFacade.GetPlayer();
            var bombs = gameFacade.GetBombs();
            var explosions = gameFacade.GetExplosions();
            
            LogAction($"Frame: Player({player?.X},{player?.Y}), Bombs:{bombs.Count}, Explosions:{explosions.Count}");
        }
        
        public void SaveReplay(string filename)
        {
            Console.WriteLine($"ReplayClient: Saving replay to {filename}...");
            Console.WriteLine($"  Total actions recorded: {actionLog.Count}");
            
            // Save actionLog to file (simplified for demonstration)
            foreach (var action in actionLog)
            {
                Console.WriteLine($"    {action}");
            }
        }
        
        private void LogAction(string action)
        {
            actionLog.Add($"[{DateTime.Now:HH:mm:ss.fff}] {action}");
        }
        
        public int GetActionCount()
        {
            return actionLog.Count;
        }
    }
    
    // FACADE PATTERN BENEFIT DEMONSTRATION
    public static class FacadePatternDemo
    {
        public static void DemonstrateFacadePattern()
        {
            Console.WriteLine("=== FACADE PATTERN DEMONSTRATION ===\n");
            Console.WriteLine("The GameFacade simplifies interaction with complex subsystems:");
            Console.WriteLine("  - PlayerManager");
            Console.WriteLine("  - BombManager");
            Console.WriteLine("  - LevelManager");
            Console.WriteLine("  - PowerUpManager");
            Console.WriteLine("  - RenderingManager");
            Console.WriteLine("  - CommandInvoker");
            Console.WriteLine("  - GameEventSystem");
            Console.WriteLine();
            
            // CLIENT 1: Main Game
            Console.WriteLine("--- CLIENT 1: MainGameClient ---");
            MainGameClient mainClient = new MainGameClient(20, 15);
            mainClient.StartGame();
            mainClient.HandlePlayerMovement(5, 0);
            mainClient.HandlePlayerAction();
            mainClient.UpdateGameState();
            Console.WriteLine();
            
            // CLIENT 2: AI Testing
            Console.WriteLine("--- CLIENT 2: AITestingClient ---");
            AITestingClient aiClient = new AITestingClient(20, 15);
            aiClient.RunAISimulation(50);
            aiClient.StressTestBombSystem(10);
            Console.WriteLine();
            
            // CLIENT 3: Replay Recording
            Console.WriteLine("--- CLIENT 3: ReplayClient ---");
            ReplayClient replayClient = new ReplayClient(20, 15);
            replayClient.RecordGameSession();
            replayClient.RecordPlayerMove(5, 0);
            replayClient.RecordPlayerMove(0, 5);
            replayClient.RecordBombPlacement();
            replayClient.RecordFrame();
            replayClient.SaveReplay("game_replay_001.dat");
            Console.WriteLine();
            
            Console.WriteLine("=== FACADE PATTERN BENEFITS ===");
            Console.WriteLine("✓ All 3 clients use the SAME facade interface");
            Console.WriteLine("✓ Clients don't need to know about internal subsystems");
            Console.WriteLine("✓ Simplified API reduces complexity for client code");
            Console.WriteLine("✓ Changes to subsystems don't affect client code");
            Console.WriteLine("✓ Each client has different needs but same simple interface");
        }
    }
}

