using BombermanGame.Entities;

namespace BombermanGame.Visitors
{
    // VISITOR PATTERN - Adapter to make entities visitable without modifying them
    public class VisitableEntity
    {
        private object entity;
        
        public VisitableEntity(object entity)
        {
            this.entity = entity;
        }
        
        public void Accept(IVisitor visitor)
        {
            switch (entity)
            {
                case Player player:
                    visitor.VisitPlayer(player);
                    break;
                case Enemy enemy:
                    visitor.VisitEnemy(enemy);
                    break;
                case Bomb bomb:
                    visitor.VisitBomb(bomb);
                    break;
                case Tile tile:
                    visitor.VisitTile(tile);
                    break;
            }
        }
        
        public object GetEntity()
        {
            return entity;
        }
    }
}

