using System;
using System.Drawing;

namespace BombermanGame.Entities
{
    public abstract class GameEntity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public bool IsActive { get; set; }
        
        protected GameEntity(int x, int y, int size)
        {
            X = x;
            Y = y;
            Size = size;
            IsActive = true;
        }
        
        public abstract void Update();
        public abstract void Render(Graphics graphics);
        
        public virtual bool CollidesWith(GameEntity other)
        {
            // Simple rectangle intersection
            Rectangle thisRect = new Rectangle(X, Y, Size, Size);
            Rectangle otherRect = new Rectangle(other.X, other.Y, other.Size, other.Size);
            return thisRect.IntersectsWith(otherRect);
        }
    }
}

