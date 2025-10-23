using System;
using System.Drawing;

namespace BombermanGame.Entities
{
    public class PowerUp : GameEntity
    {
        public PowerUpType Type { get; set; }
        
        public PowerUp(int x, int y, PowerUpType type) : base(x, y, 32)
        {
            Type = type;
        }
        
        public override void Update()
        {
            // Power-ups are static
        }
        
        public override void Render(Graphics graphics)
        {
            Color color = Type switch
            {
                PowerUpType.BombRange => Color.Orange,
                PowerUpType.BombCount => Color.Yellow,
                PowerUpType.Speed => Color.Cyan,
                _ => Color.White
            };
            
            using (Brush brush = new SolidBrush(color))
            {
                graphics.FillRectangle(brush, X + 8, Y + 8, 16, 16);
            }
            using (Pen pen = new Pen(Color.Black, 2))
            {
                graphics.DrawRectangle(pen, X + 8, Y + 8, 16, 16);
            }
        }
        
        public override GameEntity Clone()
        {
            return new PowerUp(X, Y, Type);
        }
    }
    
    public enum PowerUpType
    {
        BombRange,
        BombCount,
        Speed
    }
}

