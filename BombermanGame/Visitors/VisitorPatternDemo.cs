using System;
using System.Drawing;
using BombermanGame.Entities;
using BombermanGame.Patterns;

namespace BombermanGame.Visitors
{
    // VISITOR PATTERN - Demonstration
    public class VisitorPatternDemo
    {
        public static void DemonstrateVisitorPattern()
        {
            Console.WriteLine("=== VISITOR PATTERN DEMONSTRATION ===\n");
            
            // Create entities
            var player = new Player(100, 100);
            var enemy = new Enemy(200, 200, new BasicAIStrategy());
            var bomb = new Bomb(300, 300, 3);
            var tile = new Tile(0, 0, TileType.Wall);
            
            // Create visitable wrappers
            var visitablePlayer = new VisitableEntity(player);
            var visitableEnemy = new VisitableEntity(enemy);
            var visitableBomb = new VisitableEntity(bomb);
            var visitableTile = new VisitableEntity(tile);
            
            // Test RenderVisitor
            Console.WriteLine("--- RenderVisitor ---");
            using (var bitmap = new Bitmap(800, 600))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                var renderVisitor = new RenderVisitor(graphics);
                visitablePlayer.Accept(renderVisitor);
                visitableEnemy.Accept(renderVisitor);
                visitableBomb.Accept(renderVisitor);
                visitableTile.Accept(renderVisitor);
            }
            Console.WriteLine();
            
            // Test UpdateVisitor
            Console.WriteLine("--- UpdateVisitor ---");
            var updateVisitor = new UpdateVisitor();
            visitablePlayer.Accept(updateVisitor);
            visitableEnemy.Accept(updateVisitor);
            visitableBomb.Accept(updateVisitor);
            visitableTile.Accept(updateVisitor);
            Console.WriteLine();
            
            // Test CollisionVisitor
            Console.WriteLine("--- CollisionVisitor ---");
            var collisionVisitor = new CollisionVisitor(105, 105);
            visitablePlayer.Accept(collisionVisitor);
            visitableEnemy.Accept(collisionVisitor);
            visitableBomb.Accept(collisionVisitor);
            visitableTile.Accept(collisionVisitor);
            Console.WriteLine($"Collision found: {collisionVisitor.HasCollision()}\n");
        }
    }
}

