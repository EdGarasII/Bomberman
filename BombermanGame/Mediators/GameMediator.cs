using System;
using System.Collections.Generic;

namespace BombermanGame.Mediators
{
    // MEDIATOR PATTERN - Concrete mediator
    public class GameMediator : IMediator
    {
        private List<IParticipant> participants;
        
        public GameMediator()
        {
            participants = new List<IParticipant>();
        }
        
        public void Register(IParticipant participant)
        {
            if (!participants.Contains(participant))
            {
                participants.Add(participant);
                participant.SetMediator(this);
            }
        }
        
        public void Unregister(IParticipant participant)
        {
            participants.Remove(participant);
            participant.SetMediator(null);
        }
        
        public void Notify(object sender, string eventType, object data)
        {
            foreach (var participant in participants)
            {
                if (participant != sender)
                {
                    participant.ReceiveNotification(eventType, data);
                }
            }
        }
    }
    
    // MEDIATOR PATTERN - Participant interface
    public interface IParticipant
    {
        void SetMediator(IMediator mediator);
        void ReceiveNotification(string eventType, object data);
        string GetName();
    }
    
    // MEDIATOR PATTERN - Participant 1: Player Manager
    public class PlayerManagerParticipant : IParticipant
    {
        private IMediator mediator;
        private string name;
        
        public PlayerManagerParticipant(string name)
        {
            this.name = name;
        }
        
        public void SetMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public void ReceiveNotification(string eventType, object data)
        {
            Console.WriteLine($"[{name}] Received {eventType}: {data}");
            
            if (eventType == "BombPlaced")
            {
                Console.WriteLine($"[{name}] Updating player bomb count");
            }
        }
        
        public void PlaceBomb()
        {
            Console.WriteLine($"[{name}] Player placed bomb");
            mediator?.Notify(this, "BombPlaced", "Player bomb");
        }
        
        public string GetName()
        {
            return name;
        }
    }
    
    // MEDIATOR PATTERN - Participant 2: Bomb Manager
    public class BombManagerParticipant : IParticipant
    {
        private IMediator mediator;
        private string name;
        
        public BombManagerParticipant(string name)
        {
            this.name = name;
        }
        
        public void SetMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public void ReceiveNotification(string eventType, object data)
        {
            Console.WriteLine($"[{name}] Received {eventType}: {data}");
            
            if (eventType == "BombPlaced")
            {
                Console.WriteLine($"[{name}] Adding bomb to collection");
            }
            else if (eventType == "BombExploded")
            {
                Console.WriteLine($"[{name}] Removing exploded bomb");
            }
        }
        
        public void ExplodeBomb()
        {
            Console.WriteLine($"[{name}] Bomb exploded");
            mediator?.Notify(this, "BombExploded", "Bomb explosion");
        }
        
        public string GetName()
        {
            return name;
        }
    }
    
    // MEDIATOR PATTERN - Participant 3: Level Manager
    public class LevelManagerParticipant : IParticipant
    {
        private IMediator mediator;
        private string name;
        
        public LevelManagerParticipant(string name)
        {
            this.name = name;
        }
        
        public void SetMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public void ReceiveNotification(string eventType, object data)
        {
            Console.WriteLine($"[{name}] Received {eventType}: {data}");
            
            if (eventType == "BombExploded")
            {
                Console.WriteLine($"[{name}] Updating level tiles after explosion");
            }
        }
        
        public void UpdateLevel()
        {
            Console.WriteLine($"[{name}] Level updated");
            mediator?.Notify(this, "LevelUpdated", "Level state");
        }
        
        public string GetName()
        {
            return name;
        }
    }
}

