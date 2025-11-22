using BombermanGame.Entities;

namespace BombermanGame.Templates
{
    // TEMPLATE METHOD PATTERN - Concrete sealed class for Player updates (3rd sealed class)
    public sealed class PlayerUpdateTemplate : EntityUpdateTemplate
    {
        protected override bool ValidateEntity(GameEntity entity)
        {
            if (!base.ValidateEntity(entity))
                return false;
                
            // Additional validation for players
            return entity is Player player;
        }
        
        protected override void HandleInvalidEntity(GameEntity entity)
        {
            if (entity is Player player && !player.IsActive)
            {
                // Handle player death or deactivation
            }
        }
        
        protected override void PerformUpdate(GameEntity entity)
        {
            if (entity is Player player)
            {
                // Update player-specific logic
                // Bomb recharging is handled in Player.Update(), but we can add additional logic here
            }
        }
        
        protected override void PostUpdate(GameEntity entity)
        {
            if (entity is Player player)
            {
                // Check if player collected power-ups, etc.
                // This could trigger events
            }
        }
    }
}

