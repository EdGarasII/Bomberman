using BombermanGame.Entities;
using BombermanGame.Patterns;

namespace BombermanGame.Templates
{
    // TEMPLATE METHOD PATTERN - Concrete sealed class for Enemy updates
    public sealed class EnemyUpdateTemplate : EntityUpdateTemplate
    {
        protected override bool ValidateEntity(GameEntity entity)
        {
            if (!base.ValidateEntity(entity))
                return false;
                
            // Additional validation for enemies
            return entity is Enemy enemy && enemy.AIStrategy != null;
        }
        
        protected override void PerformUpdate(GameEntity entity)
        {
            if (entity is Enemy enemy)
            {
                // Execute AI strategy
                enemy.AIStrategy?.PerformAction(enemy);
            }
        }
        
        protected override void PostUpdate(GameEntity entity)
        {
            if (entity is Enemy enemy)
            {
                // Check if enemy is out of bounds or should be removed
                if (enemy.X < 0 || enemy.Y < 0 || enemy.X > 800 || enemy.Y > 600)
                {
                    enemy.IsActive = false;
                }
            }
        }
    }
}

