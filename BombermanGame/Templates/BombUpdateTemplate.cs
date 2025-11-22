using BombermanGame.Entities;

namespace BombermanGame.Templates
{
    // TEMPLATE METHOD PATTERN - Concrete sealed class for Bomb updates
    public sealed class BombUpdateTemplate : EntityUpdateTemplate
    {
        protected override bool ValidateEntity(GameEntity entity)
        {
            if (!base.ValidateEntity(entity))
                return false;
                
            // Additional validation for bombs
            if (entity is Bomb bomb)
            {
                return bomb.Timer > 0;
            }
            return false;
        }
        
        protected override void PerformUpdate(GameEntity entity)
        {
            if (entity is Bomb bomb)
            {
                // Decrement timer
                bomb.Timer--;
                
                // Update visual effects based on timer
                if (bomb.Timer < 60) // Last second
                {
                    // Could trigger visual warning here
                }
            }
        }
        
        protected override void PostUpdate(GameEntity entity)
        {
            if (entity is Bomb bomb && bomb.ShouldExplode())
            {
                // Mark for explosion
                bomb.IsActive = false;
            }
        }
    }
}

