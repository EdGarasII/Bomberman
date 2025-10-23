using System;
using BombermanGame.Entities;

namespace BombermanGame.Factories
{
    // ABSTRACT FACTORY PATTERN - Create families of related objects
    public abstract class AbstractEntityFactory
    {
        public abstract Player CreatePlayer(int x, int y);
        public abstract Bomb CreateBomb(int x, int y, int range);
        public abstract Explosion CreateExplosion(int x, int y, int timer);
        public abstract Tile CreateTile(int x, int y, TileType type);
    }
    
    // Standard entity factory - normal game entities
    public class StandardEntityFactory : AbstractEntityFactory
    {
        public override Player CreatePlayer(int x, int y)
        {
            return new Player(x, y);
        }
        
        public override Bomb CreateBomb(int x, int y, int range)
        {
            return new Bomb(x, y, range);
        }
        
        public override Explosion CreateExplosion(int x, int y, int timer)
        {
            return new Explosion(x, y, timer);
        }
        
        public override Tile CreateTile(int x, int y, TileType type)
        {
            return new Tile(x, y, type);
        }
    }
    
    // Enhanced entity factory - improved game entities
    public class EnhancedEntityFactory : AbstractEntityFactory
    {
        public override Player CreatePlayer(int x, int y)
        {
            var player = new Player(x, y);
            player.MaxBombs = 2; // Enhanced: more bombs
            player.BombRange = 3; // Enhanced: longer range
            return player;
        }
        
        public override Bomb CreateBomb(int x, int y, int range)
        {
            return new Bomb(x, y, range + 1); // Enhanced: longer range
        }
        
        public override Explosion CreateExplosion(int x, int y, int timer)
        {
            return new Explosion(x, y, timer);
        }
        
        public override Tile CreateTile(int x, int y, TileType type)
        {
            return new Tile(x, y, type);
        }
    }
}

