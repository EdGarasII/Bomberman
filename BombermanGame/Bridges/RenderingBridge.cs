using System;
using System.Drawing;
using BombermanGame.Entities;
using System.Collections.Generic;

namespace BombermanGame.Bridges
{
    // BRIDGE PATTERN - Separates rendering abstraction from implementation
    
    public interface IRenderer
    {
        void RenderPlayer(Graphics g, Player player);
        void RenderBomb(Graphics g, Bomb bomb);
        void RenderExplosion(Graphics g, Explosion explosion);
        void RenderTile(Graphics g, Tile tile, int tileSize);
        void RenderEnemy(Graphics g, Enemy enemy);
        void RenderPowerUp(Graphics g, PowerUp powerUp);
    }
    
    public class BasicRenderer : IRenderer
    {
        public void RenderPlayer(Graphics g, Player player)
        {
            using (Brush brush = new SolidBrush(Color.Blue))
            {
                g.FillEllipse(brush, player.X, player.Y, player.Size, player.Size);
            }
        }
        
        public void RenderBomb(Graphics g, Bomb bomb)
        {
            using (Brush brush = new SolidBrush(Color.Black))
            {
                g.FillEllipse(brush, bomb.X + 4, bomb.Y + 4, 24, 24);
            }
        }
        
        public void RenderExplosion(Graphics g, Explosion explosion)
        {
            using (Brush brush = new SolidBrush(Color.Orange))
            {
                g.FillRectangle(brush, explosion.X, explosion.Y, 32, 32);
            }
        }
        
        public void RenderTile(Graphics g, Tile tile, int tileSize)
        {
            int pixelX = tile.X * tileSize;
            int pixelY = tile.Y * tileSize;
            
            Color color = tile.Type switch
            {
                TileType.Wall => Color.DarkGray,
                TileType.BreakableWall => Color.Brown,
                _ => Color.LightGray
            };
            
            using (Brush brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, pixelX, pixelY, tileSize, tileSize);
            }
        }
        
        public void RenderEnemy(Graphics g, Enemy enemy)
        {
            using (Brush brush = new SolidBrush(Color.Red))
            {
                g.FillEllipse(brush, enemy.X, enemy.Y, enemy.Size, enemy.Size);
            }
        }
        
        public void RenderPowerUp(Graphics g, PowerUp powerUp)
        {
            using (Brush brush = new SolidBrush(Color.Yellow))
            {
                g.FillRectangle(brush, powerUp.X + 8, powerUp.Y + 8, 16, 16);
            }
        }
    }
    
    public class AdvancedRenderer : IRenderer
    {
        public void RenderPlayer(Graphics g, Player player)
        {
            using (Brush brush = new SolidBrush(Color.Blue))
            {
                g.FillEllipse(brush, player.X, player.Y, player.Size, player.Size);
            }
            using (Pen pen = new Pen(Color.DarkBlue, 2))
            {
                g.DrawEllipse(pen, player.X, player.Y, player.Size, player.Size);
            }
            
            // Add eyes
            using (Brush eyeBrush = new SolidBrush(Color.White))
            {
                g.FillEllipse(eyeBrush, player.X + 4, player.Y + 4, 3, 3);
                g.FillEllipse(eyeBrush, player.X + 13, player.Y + 4, 3, 3);
            }
        }
        
        public void RenderBomb(Graphics g, Bomb bomb)
        {
            using (Brush brush = new SolidBrush(Color.Black))
            {
                g.FillEllipse(brush, bomb.X + 4, bomb.Y + 4, 24, 24);
            }
            using (Pen pen = new Pen(Color.DarkGray, 2))
            {
                g.DrawEllipse(pen, bomb.X + 4, bomb.Y + 4, 24, 24);
            }
            
            int fuseLength = bomb.GetFuseLength();
            using (Pen fusePen = new Pen(Color.Orange, 3))
            {
                g.DrawLine(fusePen, bomb.X + 16, bomb.Y, bomb.X + 16, bomb.Y - fuseLength);
            }
        }
        
        public void RenderExplosion(Graphics g, Explosion explosion)
        {
            using (Brush brush = new SolidBrush(Color.Orange))
            {
                g.FillRectangle(brush, explosion.X, explosion.Y, 32, 32);
            }
            using (Pen pen = new Pen(Color.Red, 2))
            {
                g.DrawRectangle(pen, explosion.X, explosion.Y, 32, 32);
            }
        }
        
        public void RenderTile(Graphics g, Tile tile, int tileSize)
        {
            int pixelX = tile.X;
            int pixelY = tile.Y;
            
            switch (tile.Type)
            {
                case TileType.Empty:
                    using (Pen gridPen = new Pen(Color.DarkGray, 1))
                    {
                        g.DrawRectangle(gridPen, pixelX, pixelY, tileSize, tileSize);
                    }
                    break;
                    
                case TileType.Wall:
                    using (Brush wallBrush = new SolidBrush(Color.DarkGray))
                    {
                        g.FillRectangle(wallBrush, pixelX, pixelY, tileSize, tileSize);
                    }
                    using (Pen wallPen = new Pen(Color.Black, 2))
                    {
                        g.DrawRectangle(wallPen, pixelX, pixelY, tileSize, tileSize);
                    }
                    break;
                    
                case TileType.BreakableWall:
                    using (Brush breakableBrush = new SolidBrush(Color.Brown))
                    {
                        g.FillRectangle(breakableBrush, pixelX, pixelY, tileSize, tileSize);
                    }
                    using (Pen breakablePen = new Pen(Color.DarkRed, 2))
                    {
                        g.DrawRectangle(breakablePen, pixelX, pixelY, tileSize, tileSize);
                    }
                    break;
            }
        }
        
        public void RenderEnemy(Graphics g, Enemy enemy)
        {
            using (Brush brush = new SolidBrush(Color.Red))
            {
                g.FillEllipse(brush, enemy.X, enemy.Y, enemy.Size, enemy.Size);
            }
            using (Pen pen = new Pen(Color.DarkRed, 2))
            {
                g.DrawEllipse(pen, enemy.X, enemy.Y, enemy.Size, enemy.Size);
            }
        }
        
        public void RenderPowerUp(Graphics g, PowerUp powerUp)
        {
            Color color = powerUp.Type switch
            {
                PowerUpType.BombRange => Color.Orange,
                PowerUpType.BombCount => Color.Yellow,
                PowerUpType.Speed => Color.Cyan,
                _ => Color.White
            };
            
            using (Brush brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, powerUp.X + 8, powerUp.Y + 8, 16, 16);
            }
            using (Pen pen = new Pen(Color.Black, 2))
            {
                g.DrawRectangle(pen, powerUp.X + 8, powerUp.Y + 8, 16, 16);
            }
        }
    }
    
    public abstract class RenderingSystem
    {
        protected IRenderer renderer;
        
        public RenderingSystem(IRenderer renderer)
        {
            this.renderer = renderer;
        }
        
        public abstract void RenderGame(Graphics g, Player player, List<Bomb> bombs, List<Explosion> explosions, Tile[,] board, int tileSize, List<PowerUp>? powerUps = null, Dictionary<string, Player>? remotePlayers = null, List<Enemy>? enemies = null);
    }
    
    public class StandardRenderingSystem : RenderingSystem
    {
        public StandardRenderingSystem(IRenderer renderer) : base(renderer)
        {
        }
        
        public override void RenderGame(Graphics g, Player player, List<Bomb> bombs, List<Explosion> explosions, Tile[,] board, int tileSize, List<PowerUp>? powerUps = null, Dictionary<string, Player>? remotePlayers = null, List<Enemy>? enemies = null)
        {
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    renderer.RenderTile(g, board[x, y], tileSize);
                }
            }
            
            foreach (var bomb in bombs)
            {
                renderer.RenderBomb(g, bomb);
            }
            
            foreach (var explosion in explosions)
            {
                renderer.RenderExplosion(g, explosion);
            }
            
            if (powerUps != null)
            {
                foreach (var powerUp in powerUps)
                {
                    renderer.RenderPowerUp(g, powerUp);
                }
            }
            
            if (enemies != null)
            {
                foreach (var enemy in enemies)
                {
                    if (enemy.IsActive)
                    {
                        renderer.RenderEnemy(g, enemy);
                    }
                }
            }
            
            if (player != null)
            {
                renderer.RenderPlayer(g, player);
            }
        }
    }
    
    public class EnhancedRenderingSystem : RenderingSystem
    {
        public EnhancedRenderingSystem(IRenderer renderer) : base(renderer)
        {
            
        }
        
        public override void RenderGame(Graphics g, Player player, List<Bomb> bombs, List<Explosion> explosions, Tile[,] board, int tileSize, List<PowerUp>? powerUps = null, Dictionary<string, Player>? remotePlayers = null, List<Enemy>? enemies = null)
        {
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    renderer.RenderTile(g, board[x, y], tileSize);
                }
            }
            
            foreach (var bomb in bombs)
            {
                renderer.RenderBomb(g, bomb);
            }
            
            foreach (var explosion in explosions)
            {
                renderer.RenderExplosion(g, explosion);
            }
            
            if (powerUps != null)
            {
                foreach (var powerUp in powerUps)
                {
                    renderer.RenderPowerUp(g, powerUp);
                }
            }
            
            if (enemies != null)
            {
                foreach (var enemy in enemies)
                {
                    if (enemy.IsActive)
                    {
                        renderer.RenderEnemy(g, enemy);
                    }
                }
            }
            
            if (player != null)
            {
                renderer.RenderPlayer(g, player);
            }
        }
    }
}

