using System;
using BombermanGame.Entities;

namespace BombermanGame.Patterns
{
    // STRATEGY PATTERN - Different AI behaviors for enemies
    public interface IAIStrategy
    {
        void PerformAction(Enemy enemy);
    }
    
    // Basic AI - Random movement
    public class BasicAIStrategy : IAIStrategy
    {
        private Random random = new Random();
        private int directionTimer = 0;
        private int currentDirection = 0;
        
        public void PerformAction(Enemy enemy)
        {
            directionTimer++;
            
            if (directionTimer > 60) 
            {
                currentDirection = random.Next(4);
                directionTimer = 0;
            }
            
            switch (currentDirection)
            {
                case 0: enemy.Move(enemy.Speed, 0); break; // Right
                case 1: enemy.Move(-enemy.Speed, 0); break; // Left
                case 2: enemy.Move(0, enemy.Speed); break; // Down
                case 3: enemy.Move(0, -enemy.Speed); break; // Up
            }
        }
    }
    
    // Complex AI - Chase player
    public class AdvancedAIStrategy : IAIStrategy
    {
        private Player targetPlayer;
        
        public AdvancedAIStrategy(Player player)
        {
            targetPlayer = player;
        }
        
        public void PerformAction(Enemy enemy)
        {
            if (targetPlayer == null) return;
            
            int deltaX = targetPlayer.X - enemy.X;
            int deltaY = targetPlayer.Y - enemy.Y;
            
            if (Math.Abs(deltaX) > Math.Abs(deltaY))
            {
                enemy.Move(deltaX > 0 ? enemy.Speed : -enemy.Speed, 0);
            }
            else
            {
                enemy.Move(0, deltaY > 0 ? enemy.Speed : -enemy.Speed);
            }
        }
    }
}

