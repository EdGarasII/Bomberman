using System;
using BombermanGame.Entities;

namespace BombermanGame.Templates
{
    // TEMPLATE METHOD PATTERN - Abstract template class
    public abstract class EntityUpdateTemplate
    {
        // Template method - defines the algorithm skeleton
        public void UpdateEntity(GameEntity entity)
        {
            if (entity == null || !entity.IsActive)
                return;
                
            // Step 1: Pre-update validation
            if (!ValidateEntity(entity))
            {
                HandleInvalidEntity(entity);
                return;
            }
            
            // Step 2: Perform entity-specific update (hook method)
            PerformUpdate(entity);
            
            // Step 3: Post-update processing
            PostUpdate(entity);
        }
        
        // Hook methods - can be overridden by subclasses
        protected virtual bool ValidateEntity(GameEntity entity)
        {
            return entity != null && entity.IsActive;
        }
        
        protected virtual void HandleInvalidEntity(GameEntity entity)
        {
            // Default: do nothing
        }
        
        // Abstract method - must be implemented by subclasses
        protected abstract void PerformUpdate(GameEntity entity);
        
        protected virtual void PostUpdate(GameEntity entity)
        {
            // Default: do nothing, can be overridden
        }
    }
}

