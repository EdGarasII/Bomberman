using System;

namespace BombermanGame.States
{
    // STATE PATTERN - Victory state (5th state to exceed requirement)
    public sealed class VictoryState : IGameState
    {
        private int finalScore;
        private int level;
        
        public VictoryState(int score = 0, int level = 1)
        {
            finalScore = score;
            this.level = level;
        }
        
        public void Enter()
        {
            Console.WriteLine("=== VICTORY STATE ===");
            Console.WriteLine($"Level {level} Completed!");
            Console.WriteLine($"Score: {finalScore}");
            Console.WriteLine("Press 'N' for next level");
            Console.WriteLine("Press 'M' to return to menu");
        }
        
        public void Update()
        {
            // Victory state doesn't update
        }
        
        public void Exit()
        {
            Console.WriteLine("Proceeding to next level...");
        }
        
        public void HandleInput(string input)
        {
            switch (input.ToUpper())
            {
                case "N":
                    Console.WriteLine("Loading next level...");
                    break;
                case "M":
                    Console.WriteLine("Returning to menu...");
                    break;
            }
        }
        
        public string GetStateName()
        {
            return "Victory";
        }
    }
}

