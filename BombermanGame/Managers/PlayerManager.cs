using System;
using BombermanGame.Entities;
using BombermanGame.Factories;
using BombermanGame.Decorators;
using BombermanGame.Bridges;

namespace BombermanGame.Managers
{
    public class PlayerManager
    {
        private Player player;
        private PowerUpApplicator powerUpApplicator; // BRIDGE PATTERN
        
        public PlayerManager()
        {
            // BRIDGE PATTERN - Initialize with validated applicator and direct modification effect
            IPowerUpEffect effect = new DirectModificationEffect();
            powerUpApplicator = new ValidatedPowerUpApplicator(effect);
        }
        
        public void CreatePlayer(int x, int y)
        {
            player = EntityFactory.CreatePlayer(x, y);
        }
        
        public Player GetPlayer() => player;
        
        public void ApplyPowerUp(PowerUpType type)
        {
            if (player == null) return;
            
            // BRIDGE PATTERN - Use bridge pattern for power-up application
            powerUpApplicator.ApplyPowerUp(player, type);
            
            // Also keep decorator pattern for compatibility
            var decorator = PowerUpDecoratorFactory.CreateDecorator(type, player);
            decorator?.ApplyPowerUp();
        }
        
        public void SetPowerUpApplicator(PowerUpApplicator applicator)
        {
            powerUpApplicator = applicator ?? throw new ArgumentNullException(nameof(applicator));
        }
        
        public bool IsValidPosition(int x, int y, int size, Tile[,] board, int tileSize)
        {
            int boardWidth = board.GetLength(0);
            int boardHeight = board.GetLength(1);
            
            if (x < 0 || y < 0 || x + size >= boardWidth * tileSize || y + size >= boardHeight * tileSize)
                return false;
            
            int tileX = x / tileSize;
            int tileY = y / tileSize;
            int tileX2 = (x + size - 1) / tileSize;
            int tileY2 = (y + size - 1) / tileSize;
            
            if (board[tileX, tileY].IsWall() ||
                board[tileX2, tileY].IsWall() ||
                board[tileX, tileY2].IsWall() ||
                board[tileX2, tileY2].IsWall())
            {
                return false;
            }
            
            return true;
        }
    }
}

