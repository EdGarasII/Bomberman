using BombermanGame.Entities;

namespace BombermanGame.Visitors
{
    // VISITOR PATTERN - Visitor 2: Update visitor
    public class UpdateVisitor : IVisitor
    {
        public void VisitPlayer(Player player)
        {
            player.Update();
            Console.WriteLine($"[UpdateVisitor] Updated player: BombCount={player.BombCount}, Speed={player.Speed}");
        }
        
        public void VisitEnemy(Enemy enemy)
        {
            enemy.Update();
            Console.WriteLine($"[UpdateVisitor] Updated enemy at ({enemy.X}, {enemy.Y})");
        }
        
        public void VisitBomb(Bomb bomb)
        {
            bomb.Update();
            Console.WriteLine($"[UpdateVisitor] Updated bomb: Timer={bomb.Timer}, Range={bomb.Range}");
        }
        
        public void VisitTile(Tile tile)
        {
            tile.Update();
            Console.WriteLine($"[UpdateVisitor] Updated tile: Type={tile.Type}");
        }
    }
}

