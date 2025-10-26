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
    
    // Invincibility power-up decorator (for 3-level stacking demonstration)
    public class InvincibilityPowerUpDecorator : PowerUpDecorator
    {
        private bool wasInvincible;
        
        public InvincibilityPowerUpDecorator(Player player) : base(player)
        {
        }
        
        public override void ApplyPowerUp()
        {
            // Store previous state and apply invincibility
            wasInvincible = false; // Could be stored in player if we had this property
            // Invincibility logic would go here
        }
        
        public override void RemovePowerUp()
        {
            // Remove invincibility
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
    
    // DECORATOR PATTERN - Demonstration of 3-level stacking
    // This class demonstrates how to apply multiple decorators in sequence
    public class PowerUpDecoratorStack
    {
        private Player player;
        private PowerUpDecorator? level1Decorator;
        private PowerUpDecorator? level2Decorator;
        private PowerUpDecorator? level3Decorator;
        
        public PowerUpDecoratorStack(Player player)
        {
            this.player = player;
        }
        
        // Apply 3 levels of decoration
        public void ApplyThreeLevelStack()
        {
            // LEVEL 1: Speed boost
            level1Decorator = new SpeedPowerUpDecorator(player);
            level1Decorator.ApplyPowerUp();
            
            // LEVEL 2: Bomb count increase (stacked on level 1)
            level2Decorator = new BombCountPowerUpDecorator(player);
            level2Decorator.ApplyPowerUp();
            
            // LEVEL 3: Bomb range increase (stacked on levels 1 and 2)
            level3Decorator = new BombRangePowerUpDecorator(player);
            level3Decorator.ApplyPowerUp();
            
            // At this point, the player has:
            // - Increased speed (Level 1)
            // - Increased bomb count (Level 2)
            // - Increased bomb range (Level 3)
        }
        
        // Remove decorations in reverse order (LIFO)
        public void RemoveThreeLevelStack()
        {
            // Remove in reverse order to maintain consistency
            if (level3Decorator != null)
            {
                level3Decorator.RemovePowerUp();
                level3Decorator = null;
            }
            
            if (level2Decorator != null)
            {
                level2Decorator.RemovePowerUp();
                level2Decorator = null;
            }
            
            if (level1Decorator != null)
            {
                level1Decorator.RemovePowerUp();
                level1Decorator = null;
            }
        }
        
        // Alternative: Apply custom 3-level combination
        public void ApplyCustomStack(PowerUpDecorator decorator1, PowerUpDecorator decorator2, PowerUpDecorator decorator3)
        {
            level1Decorator = decorator1;
            level2Decorator = decorator2;
            level3Decorator = decorator3;
            
            level1Decorator?.ApplyPowerUp();
            level2Decorator?.ApplyPowerUp();
            level3Decorator?.ApplyPowerUp();
        }
        
        public int GetStackDepth()
        {
            int depth = 0;
            if (level1Decorator != null) depth++;
            if (level2Decorator != null) depth++;
            if (level3Decorator != null) depth++;
            return depth;
        }
    }
    
    // Alternative approach: Nested decorator wrapping (more traditional decorator pattern)
    public class NestedDecoratorExample
    {
        // This demonstrates the classic nested decorator approach
        public static void DemonstrateNestedDecorators(Player player)
        {
            // Create a base decorator
            PowerUpDecorator baseDecorator = new SpeedPowerUpDecorator(player);
            
            // Wrap it with a second decorator
            PowerUpDecorator secondDecorator = new BombCountPowerUpDecorator(player);
            
            // Wrap with a third decorator
            PowerUpDecorator thirdDecorator = new BombRangePowerUpDecorator(player);
            
            // Apply all three levels
            baseDecorator.ApplyPowerUp();      // Level 1: Speed
            secondDecorator.ApplyPowerUp();    // Level 2: Bomb Count
            thirdDecorator.ApplyPowerUp();     // Level 3: Bomb Range
            
            // Now the player has all three power-ups applied in layers
            // Speed: +1, MaxBombs: +1, BombRange: +1
        }
    }
}

