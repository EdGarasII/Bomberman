using System;
using BombermanGame.Entities;

namespace BombermanGame.Visitors
{
    // VISITOR PATTERN - Visitor 3: Collision detection visitor
    public class CollisionVisitor : IVisitor
    {
        private int checkX, checkY;
        private bool collisionFound;
        
        public CollisionVisitor(int x, int y)
        {
            checkX = x;
            checkY = y;
            collisionFound = false;
        }
        
        public bool HasCollision()
        {
            return collisionFound;
        }
        
        public void VisitPlayer(Player player)
        {
            if (IsColliding(player.X, player.Y, player.Size))
            {
                collisionFound = true;
                Console.WriteLine($"[CollisionVisitor] Collision with player at ({player.X}, {player.Y})");
            }
        }
        
        public void VisitEnemy(Enemy enemy)
        {
            if (IsColliding(enemy.X, enemy.Y, enemy.Size))
            {
                collisionFound = true;
                Console.WriteLine($"[CollisionVisitor] Collision with enemy at ({enemy.X}, {enemy.Y})");
            }
        }
        
        public void VisitBomb(Bomb bomb)
        {
            if (IsColliding(bomb.X, bomb.Y, bomb.Size))
            {
                collisionFound = true;
                Console.WriteLine($"[CollisionVisitor] Collision with bomb at ({bomb.X}, {bomb.Y})");
            }
        }
        
        public void VisitTile(Tile tile)
        {
            if (!tile.IsPassable() && IsColliding(tile.X, tile.Y, tile.Size))
            {
                collisionFound = true;
                Console.WriteLine($"[CollisionVisitor] Collision with tile at ({tile.X}, {tile.Y})");
            }
        }
        
        private bool IsColliding(int x, int y, int size)
        {
            return checkX >= x && checkX < x + size &&
                   checkY >= y && checkY < y + size;
        }
    }
}

