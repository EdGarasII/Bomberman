using System;
using System.Drawing;
using BombermanGame.Patterns;
using BombermanGame.Prototypes;

namespace BombermanGame.Entities
{
    // PROTOTYPE PATTERN - Enemy implements IPrototype<Enemy>
    public class Enemy : GameEntity, IPrototype<Enemy>
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
        
        public Enemy Clone()
        {
            return (Enemy)this.MemberwiseClone();
        }
        
        public Enemy DeepClone()
        {
            IAIStrategy? newStrategy = null;
            if (AIStrategy is BasicAIStrategy)
            {
                newStrategy = new BasicAIStrategy();
            }
            else if (AIStrategy is AdvancedAIStrategy advancedStrategy)
            {
                newStrategy = AIStrategy;
            }
            else
            {
                newStrategy = AIStrategy;
            }
            
            var clone = new Enemy(X, Y, newStrategy ?? new BasicAIStrategy());
            clone.Speed = this.Speed;
            clone.IsActive = this.IsActive;
            return clone;
        }
        
        public void Move(int deltaX, int deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }
    }
}

