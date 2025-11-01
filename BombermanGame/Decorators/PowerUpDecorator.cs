using System;
using BombermanGame.Entities;

namespace BombermanGame.Decorators
{
    // DECORATOR PATTERN - Enhances player abilities with power-ups
    public abstract class PowerUpDecorator
    {
        protected Player player;
        
        public PowerUpDecorator(Player player)
        {
            this.player = player;
        }
        
        public abstract void ApplyPowerUp();
        public abstract void RemovePowerUp();
    }
    
    // Speed power-up decorator
    public class SpeedPowerUpDecorator : PowerUpDecorator
    {
        private int previousSpeed;
        
        public SpeedPowerUpDecorator(Player player) : base(player)
        {
        }
        
        public override void ApplyPowerUp()
        {
            previousSpeed = player.Speed;
            player.Speed += 1;
        }
        
        public override void RemovePowerUp()
        {
            player.Speed = previousSpeed;
        }
    }
    
    // Bomb count power-up decorator
    public class BombCountPowerUpDecorator : PowerUpDecorator
    {
        public BombCountPowerUpDecorator(Player player) : base(player)
        {
        }
        
        public override void ApplyPowerUp()
        {
            player.MaxBombs++;
            player.BombCount++;
        }
        
        public override void RemovePowerUp()
        {
            player.MaxBombs--;
            if (player.BombCount > player.MaxBombs)
            {
                player.BombCount = player.MaxBombs;
            }
        }
    }
    
    // Bomb range power-up decorator
    public class BombRangePowerUpDecorator : PowerUpDecorator
    {
        public BombRangePowerUpDecorator(Player player) : base(player)
        {
        }
        
        public override void ApplyPowerUp()
        {
            player.BombRange++;
        }
        
        public override void RemovePowerUp()
        {
            player.BombRange--;
        }
    }
    
    // Factory for creating power-up decorators
    public class PowerUpDecoratorFactory
    {
        public static PowerUpDecorator CreateDecorator(PowerUpType type, Player player)
        {
            return type switch
            {
                PowerUpType.Speed => new SpeedPowerUpDecorator(player),
                PowerUpType.BombCount => new BombCountPowerUpDecorator(player),
                PowerUpType.BombRange => new BombRangePowerUpDecorator(player),
                _ => null
            };
        }
    }
}

