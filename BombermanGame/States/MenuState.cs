using System;

namespace BombermanGame.States
{
    // STATE PATTERN - Menu state (initial state)
    public sealed class MenuState : IGameState
    {
        public void Enter()
        {
            Console.WriteLine("=== MENU STATE ===");
            Console.WriteLine("Press 'S' to start game");
            Console.WriteLine("Press 'Q' to quit");
        }
        
        public void Update()
        {
            // Menu doesn't need continuous updates
        }
        
        public void Exit()
        {
            Console.WriteLine("Exiting menu...");
        }
        
        public void HandleInput(string input)
        {
            switch (input.ToUpper())
            {
                case "S":
                    Console.WriteLine("Starting game...");
                    break;
                case "Q":
                    Console.WriteLine("Quitting...");
                    break;
            }
        }
        
        public string GetStateName()
        {
            return "Menu";
        }
    }
}

