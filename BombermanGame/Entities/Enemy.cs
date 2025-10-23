using System;
using System.Drawing;
using BombermanGame.Patterns;

namespace BombermanGame.Entities
{
    public class Enemy : GameEntity
    {
        public int Speed { get; set; }
        public IAIStrategy AIStrategy { get; set; }
        
        public Enemy(int x, int y, IAIStrategy aiStrategy) : base(x, y, 20)
        {
            Speed = 1;
            AIStrategy = aiStrategy;
        }
        
        public override void Update()
        {
            if (AIStrategy != null)
            {
                AIStrategy.PerformAction(this);
            }
        }
        
        public override void Render(Graphics graphics)
        {
            using (Brush enemyBrush = new SolidBrush(Color.Red))
            {
                graphics.FillEllipse(enemyBrush, X, Y, Size, Size);
            }
            using (Pen enemyPen = new Pen(Color.DarkRed, 2))
            {
                graphics.DrawEllipse(enemyPen, X, Y, Size, Size);
            }
        }
        
        public override GameEntity Clone()
        {
            return new Enemy(X, Y, AIStrategy);
        }
        
        public void Move(int deltaX, int deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }
    }
}

