using System;
using System.Collections.Generic;

namespace BombermanGame.Observers
{
    // OBSERVER PATTERN - Event notification system
    public sealed class GameEventSystem
    {
        private static GameEventSystem instance = null;
        private static readonly object padlock = new object();
        
        private Dictionary<GameEventType, List<IEventHandler>> eventHandlers;
        
        private GameEventSystem()
        {
            eventHandlers = new Dictionary<GameEventType, List<IEventHandler>>();
        }
        
        public static GameEventSystem Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GameEventSystem();
                    }
                    return instance;
                }
            }
        }
        
        public void Subscribe(GameEventType eventType, IEventHandler handler)
        {
            if (!eventHandlers.ContainsKey(eventType))
            {
                eventHandlers[eventType] = new List<IEventHandler>();
            }
            
            if (!eventHandlers[eventType].Contains(handler))
            {
                eventHandlers[eventType].Add(handler);
            }
        }
        
        public void Unsubscribe(GameEventType eventType, IEventHandler handler)
        {
            if (eventHandlers.ContainsKey(eventType))
            {
                eventHandlers[eventType].Remove(handler);
            }
        }
        
        public void Notify(GameEventType eventType, GameEventData data)
        {
            if (eventHandlers.ContainsKey(eventType))
            {
                foreach (var handler in eventHandlers[eventType])
                {
                    handler.HandleEvent(eventType, data);
                }
            }
        }
    }
    
    // Event handler interface
    public interface IEventHandler
    {
        void HandleEvent(GameEventType eventType, GameEventData data);
    }
    
    // Game event types
    public enum GameEventType
    {
        BombExploded,
        PlayerDied,
        EnemyDied,
        PowerUpCollected,
        WallDestroyed,
        LevelCompleted,
        ScoreChanged
    }
    
    // Event data container
    public class GameEventData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public object AdditionalData { get; set; }
        
        public GameEventData(int x, int y, int value = 0, object additionalData = null)
        {
            X = x;
            Y = y;
            Value = value;
            AdditionalData = additionalData;
        }
    }
}

