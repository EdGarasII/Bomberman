using System;

namespace BombermanGame.Core
{
    // SINGLETON PATTERN - Ensures only one game instance exists
    public sealed class GameManager
    {
        private static GameManager instance = null;
        private static readonly object padlock = new object();
        
        public GameState CurrentState { get; private set; }
        public int Score { get; set; }
        public int Level { get; set; }
        
        private GameManager()
        {
            CurrentState = GameState.Menu;
            Score = 0;
            Level = 1;
        }
        
        public static GameManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GameManager();
                    }
                    return instance;
                }
            }
        }
        
        public void SetState(GameState newState)
        {
            CurrentState = newState;
        }
        
        public void AddScore(int points)
        {
            Score += points;
        }
        
        public void NextLevel()
        {
            Level++;
        }
        
        public void Reset()
        {
            Score = 0;
            Level = 1;
            CurrentState = GameState.Menu;
        }
    }
}

