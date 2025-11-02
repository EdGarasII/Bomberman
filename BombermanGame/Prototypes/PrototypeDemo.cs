using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BombermanGame.Entities;

namespace BombermanGame.Prototypes
{
    
    public class ComplexPlayer : GameEntity
    {
        public int Speed { get; set; }
        public List<string> Inventory { get; set; }
        public PlayerStats Stats { get; set; }     
        
        public ComplexPlayer(int x, int y) : base(x, y, 20)
        {
            Speed = 2;
            Inventory = new List<string>();
            Stats = new PlayerStats();
        }
        
        public ComplexPlayer ShallowCopy()
        {
            return (ComplexPlayer)this.MemberwiseClone();
        }
        
        public ComplexPlayer DeepCopy()
        {
            var deepCopy = new ComplexPlayer(this.X, this.Y);
            deepCopy.Speed = this.Speed;
            deepCopy.Size = this.Size;
            deepCopy.IsActive = this.IsActive;
            
            deepCopy.Inventory = new List<string>(this.Inventory);
            deepCopy.Stats = new PlayerStats
            {
                Health = this.Stats.Health,
                Score = this.Stats.Score,
                Level = this.Stats.Level
            };
            
            return deepCopy;
        }
        
        public override void Update() { }
        
        public override void Render(System.Drawing.Graphics graphics) { }
        
        public override GameEntity Clone()
        {
            return DeepCopy();
        }
    }
    
    public class PlayerStats
    {
        public int Health { get; set; } = 100;
        public int Score { get; set; } = 0;
        public int Level { get; set; } = 1;
    }
}

