using System;

namespace BombermanGame.States
{
    // STATE PATTERN - Playing state (active game)
    public sealed class PlayingState : IGameState
    {
        private int gameTime;
        
        public void Enter()
        {
            Console.WriteLine("=== PLAYING STATE ===");
            Console.WriteLine("Game started!");
            gameTime = 0;
        }
        
        public void Update()
        {
            gameTime++;
            // Simulate game loop
            if (gameTime % 100 == 0)
            {
                Console.WriteLine($"Game running... Time: {gameTime}");
            }
        }
        
        public void Exit()
        {
            Console.WriteLine("Exiting playing state...");
        }
        
        public void HandleInput(string input)
        {
            switch (input.ToUpper())
            {
                case "P":
                    Console.WriteLine("Pausing game...");
                    break;
                case "ESC":
                    Console.WriteLine("Returning to menu...");
                    break;
            }
        }
        
        public string GetStateName()
        {
            return "Playing";
        }
    }
}

