using System;
using BombermanGame.Entities;
using BombermanGame.Patterns;

namespace BombermanGame.Factories
{
    // FACTORY PATTERN - Create objects without specifying their exact classes
    public class EntityFactory
    {
        public static Player CreatePlayer(int x, int y)
        {
            return new Player(x, y);
        }
        
        public static Bomb CreateBomb(int x, int y, int range)
        {
            return new Bomb(x, y, range);
        }
        
        public static Explosion CreateExplosion(int x, int y, int timer)
        {
            return new Explosion(x, y, timer);
        }
        
        public static Tile CreateTile(int x, int y, TileType type)
        {
            return new Tile(x, y, type);
        }
        
        public static Enemy CreateEnemy(int x, int y, IAIStrategy aiStrategy)
        {
            return new Enemy(x, y, aiStrategy);
        }
        
        public static PowerUp CreatePowerUp(int x, int y, PowerUpType type)
        {
            return new PowerUp(x, y, type);
        }
    }
}

