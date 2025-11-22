using System;

namespace BombermanGame.States
{
    // STATE PATTERN - Demonstration class
    public class StatePatternDemo
    {
        public static void DemonstrateStatePattern()
        {
            Console.WriteLine("=== STATE PATTERN DEMONSTRATION ===\n");
            
            // Create initial state (Menu)
            var menuState = new MenuState();
            var context = new GameStateContext(menuState);
            
            Console.WriteLine($"Current State: {context.GetCurrentStateName()}\n");
            
            // Transition to Playing state
            Console.WriteLine("--- Transitioning to Playing State ---");
            context.ChangeState(new PlayingState());
            Console.WriteLine($"Current State: {context.GetCurrentStateName()}\n");
            
            // Simulate some updates
            for (int i = 0; i < 3; i++)
            {
                context.Update();
            }
            
            // Transition to Paused state
            Console.WriteLine("\n--- Transitioning to Paused State ---");
            context.ChangeState(new PausedState());
            Console.WriteLine($"Current State: {context.GetCurrentStateName()}\n");
            
            // Transition to GameOver state
            Console.WriteLine("--- Transitioning to GameOver State ---");
            context.ChangeState(new GameOverState(1500));
            Console.WriteLine($"Current State: {context.GetCurrentStateName()}\n");
            
            // Transition to Victory state
            Console.WriteLine("--- Transitioning to Victory State ---");
            context.ChangeState(new VictoryState(2000, 2));
            Console.WriteLine($"Current State: {context.GetCurrentStateName()}\n");
            
            // Return to Menu
            Console.WriteLine("--- Returning to Menu State ---");
            context.ChangeState(new MenuState());
            Console.WriteLine($"Current State: {context.GetCurrentStateName()}\n");
        }
    }
}

