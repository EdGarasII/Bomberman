using System;

namespace BombermanGame.States
{
    // STATE PATTERN - Game Over state
    public sealed class GameOverState : IGameState
    {
        private int finalScore;
        
        public GameOverState(int score = 0)
        {
            finalScore = score;
        }
        
        public void Enter()
        {
            Console.WriteLine("=== GAME OVER STATE ===");
            Console.WriteLine($"Final Score: {finalScore}");
            Console.WriteLine("Press 'R' to restart");
            Console.WriteLine("Press 'M' to return to menu");
        }
        
        public void Update()
        {
            // Game over state doesn't update
        }
        
        public void Exit()
        {
            Console.WriteLine("Exiting game over screen...");
        }
        
        public void HandleInput(string input)
        {
            switch (input.ToUpper())
            {
                case "R":
                    Console.WriteLine("Restarting game...");
                    break;
                case "M":
                    Console.WriteLine("Returning to menu...");
                    break;
            }
        }
        
        public string GetStateName()
        {
            return "GameOver";
        }
    }
}

