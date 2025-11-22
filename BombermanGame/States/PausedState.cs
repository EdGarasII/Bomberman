using System;

namespace BombermanGame.States
{
    // STATE PATTERN - Paused state
    public sealed class PausedState : IGameState
    {
        public void Enter()
        {
            Console.WriteLine("=== PAUSED STATE ===");
            Console.WriteLine("Game is paused");
            Console.WriteLine("Press 'R' to resume");
            Console.WriteLine("Press 'M' to return to menu");
        }
        
        public void Update()
        {
            // Paused state doesn't update game logic
        }
        
        public void Exit()
        {
            Console.WriteLine("Resuming game...");
        }
        
        public void HandleInput(string input)
        {
            switch (input.ToUpper())
            {
                case "R":
                    Console.WriteLine("Resuming...");
                    break;
                case "M":
                    Console.WriteLine("Returning to menu...");
                    break;
            }
        }
        
        public string GetStateName()
        {
            return "Paused";
        }
    }
}

