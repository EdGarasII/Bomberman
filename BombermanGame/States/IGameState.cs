namespace BombermanGame.States
{
    // STATE PATTERN - Defines interface for game states
    public interface IGameState
    {
        void Enter();
        void Update();
        void Exit();
        void HandleInput(string input);
        string GetStateName();
    }
}

