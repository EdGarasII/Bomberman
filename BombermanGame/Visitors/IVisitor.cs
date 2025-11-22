using BombermanGame.Entities;

namespace BombermanGame.Visitors
{
    // VISITOR PATTERN - Visitor interface
    public interface IVisitor
    {
        void VisitPlayer(Player player);
        void VisitEnemy(Enemy enemy);
        void VisitBomb(Bomb bomb);
        void VisitTile(Tile tile);
    }
    
    // VISITOR PATTERN - Element interface
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}

