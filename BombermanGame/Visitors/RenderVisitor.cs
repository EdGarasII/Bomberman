using System.Drawing;
using BombermanGame.Entities;

namespace BombermanGame.Visitors
{
    // VISITOR PATTERN - Visitor 1: Rendering visitor
    public class RenderVisitor : IVisitor
    {
        private Graphics graphics;
        
        public RenderVisitor(Graphics g)
        {
            graphics = g;
        }
        
        public void VisitPlayer(Player player)
        {
            using (Brush brush = new SolidBrush(Color.Blue))
            {
                graphics.FillEllipse(brush, player.X, player.Y, player.Size, player.Size);
            }
            Console.WriteLine($"[RenderVisitor] Rendered player at ({player.X}, {player.Y})");
        }
        
        public void VisitEnemy(Enemy enemy)
        {
            using (Brush brush = new SolidBrush(Color.Red))
            {
                graphics.FillEllipse(brush, enemy.X, enemy.Y, enemy.Size, enemy.Size);
            }
            Console.WriteLine($"[RenderVisitor] Rendered enemy at ({enemy.X}, {enemy.Y})");
        }
        
        public void VisitBomb(Bomb bomb)
        {
            using (Brush brush = new SolidBrush(Color.Black))
            {
                graphics.FillEllipse(brush, bomb.X, bomb.Y, bomb.Size, bomb.Size);
            }
            Console.WriteLine($"[RenderVisitor] Rendered bomb at ({bomb.X}, {bomb.Y})");
        }
        
        public void VisitTile(Tile tile)
        {
            Color color = tile.Type switch
            {
                TileType.Wall => Color.DarkGray,
                TileType.BreakableWall => Color.Brown,
                _ => Color.LightGray
            };
            
            using (Brush brush = new SolidBrush(color))
            {
                graphics.FillRectangle(brush, tile.X, tile.Y, tile.Size, tile.Size);
            }
            Console.WriteLine($"[RenderVisitor] Rendered tile at ({tile.X}, {tile.Y})");
        }
    }
}

