using System;
using System.Drawing;
using System.Collections.Generic;
using BombermanGame.Entities;

namespace BombermanGame.Managers
{
    public class RenderingManager
    {
        private const int TILE_SIZE = 32;
        
        public RenderingManager()
        {
        }
        
        public void Render(Graphics g, Player player, List<Bomb> bombs, List<Explosion> explosions, Tile[,] board, List<PowerUp>? powerUps = null, Dictionary<string, Player>? remotePlayers = null, List<Enemy>? enemies = null)
        {
            // Render board
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    int pixelX = x * TILE_SIZE;
                    int pixelY = y * TILE_SIZE;
                    
                    switch (board[x, y].Type)
                    {
                        case TileType.Empty:
                            using (Pen gridPen = new Pen(Color.DarkGray, 1))
                            {
                                g.DrawRectangle(gridPen, pixelX, pixelY, TILE_SIZE, TILE_SIZE);
                            }
                            break;
                            
                        case TileType.Wall:
                            using (Brush wallBrush = new SolidBrush(Color.DarkGray))
                            {
                                g.FillRectangle(wallBrush, pixelX, pixelY, TILE_SIZE, TILE_SIZE);
                            }
                            using (Pen wallPen = new Pen(Color.Black, 2))
                            {
                                g.DrawRectangle(wallPen, pixelX, pixelY, TILE_SIZE, TILE_SIZE);
                            }
                            break;
                            
                        case TileType.BreakableWall:
                            using (Brush breakableBrush = new SolidBrush(Color.Brown))
                            {
                                g.FillRectangle(breakableBrush, pixelX, pixelY, TILE_SIZE, TILE_SIZE);
                            }
                            using (Pen breakablePen = new Pen(Color.DarkRed, 2))
                            {
                                g.DrawRectangle(breakablePen, pixelX, pixelY, TILE_SIZE, TILE_SIZE);
                            }
                            break;
                    }
                }
            }
            
            // Render power-ups
            if (powerUps != null)
            {
                foreach (var powerUp in powerUps)
                {
                    powerUp.Render(g);
                }
            }
            
            // Render bombs
            foreach (var bomb in bombs)
            {
                using (Brush bombBrush = new SolidBrush(Color.Black))
                {
                    g.FillEllipse(bombBrush, bomb.X + 4, bomb.Y + 4, 24, 24);
                }
                using (Pen bombPen = new Pen(Color.DarkGray, 2))
                {
                    g.DrawEllipse(bombPen, bomb.X + 4, bomb.Y + 4, 24, 24);
                }
                
                int fuseLength = bomb.GetFuseLength();
                using (Pen fusePen = new Pen(Color.Orange, 3))
                {
                    g.DrawLine(fusePen, bomb.X + 16, bomb.Y, bomb.X + 16, bomb.Y - fuseLength);
                }
            }
            
            // Render explosions
            foreach (var explosion in explosions)
            {
                using (Brush explosionBrush = new SolidBrush(Color.Orange))
                {
                    g.FillRectangle(explosionBrush, explosion.X, explosion.Y, TILE_SIZE, TILE_SIZE);
                }
                using (Pen explosionPen = new Pen(Color.Red, 2))
                {
                    g.DrawRectangle(explosionPen, explosion.X, explosion.Y, TILE_SIZE, TILE_SIZE);
                }
            }
            
            // Render enemies
            if (enemies != null)
            {
                foreach (var enemy in enemies)
                {
                    if (enemy.IsActive)
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
                }
            }
            
            // Render player
            if (player != null && player.IsActive)
            {
                using (Brush playerBrush = new SolidBrush(Color.Blue))
                {
                    g.FillEllipse(playerBrush, player.X, player.Y, player.Size, player.Size);
                }
                using (Pen playerPen = new Pen(Color.DarkBlue, 2))
                {
                    g.DrawEllipse(playerPen, player.X, player.Y, player.Size, player.Size);
                }
                
                using (Brush eyeBrush = new SolidBrush(Color.White))
                {
                    g.FillEllipse(eyeBrush, player.X + 4, player.Y + 4, 3, 3);
                    g.FillEllipse(eyeBrush, player.X + 13, player.Y + 4, 3, 3);
                }
                using (Brush pupilBrush = new SolidBrush(Color.Black))
                {
                    g.FillEllipse(pupilBrush, player.X + 5, player.Y + 5, 1, 1);
                    g.FillEllipse(pupilBrush, player.X + 14, player.Y + 5, 1, 1);
                }
            }
        }
    }
}

