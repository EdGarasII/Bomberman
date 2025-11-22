using System;
using System.Collections.Generic;

namespace BombermanGame.Mementos
{
    // MEMENTO PATTERN - Memento (stores state)
    public class GameMemento
    {
        private Dictionary<string, object> state;
        private DateTime timestamp;
        
        // Private constructor - only Originator can create
        internal GameMemento(Dictionary<string, object> state)
        {
            this.state = new Dictionary<string, object>(state);
            timestamp = DateTime.Now;
        }
        
        // Internal access - only Originator can restore
        internal Dictionary<string, object> GetState()
        {
            return new Dictionary<string, object>(state);
        }
        
        public DateTime GetTimestamp()
        {
            return timestamp;
        }
    }
    
    // MEMENTO PATTERN - Originator (creates and restores mementos)
    public class GameOriginator
    {
        private Dictionary<string, object> gameState;
        
        public GameOriginator()
        {
            gameState = new Dictionary<string, object>();
        }
        
        public void SetState(string key, object value)
        {
            gameState[key] = value;
        }
        
        public object GetState(string key)
        {
            return gameState.ContainsKey(key) ? gameState[key] : null;
        }
        
        // Create memento - only originator can do this
        public GameMemento CreateMemento()
        {
            return new GameMemento(gameState);
        }
        
        // Restore from memento - only originator can do this
        public void RestoreFromMemento(GameMemento memento)
        {
            if (memento == null)
                throw new ArgumentNullException(nameof(memento));
                
            gameState = memento.GetState();
        }
        
        public void DisplayState()
        {
            Console.WriteLine("Current Game State:");
            foreach (var kvp in gameState)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }
        }
    }
    
    // MEMENTO PATTERN - Caretaker (stores mementos, but cannot access state)
    public class GameCaretaker
    {
        private List<GameMemento> mementos;
        
        public GameCaretaker()
        {
            mementos = new List<GameMemento>();
        }
        
        public void SaveMemento(GameMemento memento)
        {
            if (memento == null)
                throw new ArgumentNullException(nameof(memento));
                
            mementos.Add(memento);
            Console.WriteLine($"Memento saved at {memento.GetTimestamp()}");
        }
        
        public GameMemento GetMemento(int index)
        {
            if (index < 0 || index >= mementos.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
                
            return mementos[index];
        }
        
        public int GetMementoCount()
        {
            return mementos.Count;
        }
        
        // Caretaker cannot access memento state - this is secure
        // Only originator can restore from memento
    }
}

