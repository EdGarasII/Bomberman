using System;

namespace BombermanGame.States
{
    // STATE PATTERN - Context class that maintains current state
    public class GameStateContext
    {
        private IGameState currentState;
        
        public GameStateContext(IGameState initialState)
        {
            currentState = initialState ?? throw new ArgumentNullException(nameof(initialState));
            currentState.Enter();
        }
        
        public void ChangeState(IGameState newState)
        {
            if (newState == null)
                throw new ArgumentNullException(nameof(newState));
                
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }
        
        public void Update()
        {
            currentState?.Update();
        }
        
        public void HandleInput(string input)
        {
            currentState?.HandleInput(input);
        }
        
        public IGameState GetCurrentState()
        {
            return currentState;
        }
        
        public string GetCurrentStateName()
        {
            return currentState?.GetStateName() ?? "Unknown";
        }
    }
}

