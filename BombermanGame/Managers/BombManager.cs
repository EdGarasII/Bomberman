using System;
using System.Collections.Generic;
using BombermanGame.Entities;
using BombermanGame.Factories;
using BombermanGame.Observers;

namespace BombermanGame.Managers
{
    public class BombManager
    {
        private List<Bomb> bombs;
        private AbstractEntityFactory entityFactory;
        private GameEventSystem eventSystem;
        
        public BombManager()
        {
            bombs = new List<Bomb>();
            entityFactory = new StandardEntityFactory();
            eventSystem = GameEventSystem.Instance;
        }
        
        public void AddBomb(int x, int y, int range)
        {
            bombs.Add(entityFactory.CreateBomb(x, y, range));
        }
        
        public void Update(Tile[,] board, List<Explosion> explosions, int tileSize)
        {
            for (int i = bombs.Count - 1; i >= 0; i--)
            {
                bombs[i].Update();
                if (bombs[i].ShouldExplode())
                {
                    ExplodeBomb(bombs[i], board, explosions, tileSize);
                    bombs.RemoveAt(i);
                }
            }
        }
        
        private void ExplodeBomb(Bomb bomb, Tile[,] board, List<Explosion> explosions, int tileSize)
        {
            int centerX = bomb.X / tileSize;
            int centerY = bomb.Y / tileSize;
            
            explosions.Add(entityFactory.CreateExplosion(centerX * tileSize, centerY * tileSize, 30));
            
            eventSystem.Notify(GameEventType.BombExploded, new GameEventData(bomb.X, bomb.Y));
            
            int boardWidth = board.GetLength(0);
            int boardHeight = board.GetLength(1);
            
            for (int direction = 0; direction < 4; direction++)
            {
                for (int distance = 1; distance <= bomb.Range; distance++)
                {
                    int x = centerX;
                    int y = centerY;
                    
                    switch (direction)
                    {
                        case 0: x += distance; break;
                        case 1: x -= distance; break;
                        case 2: y += distance; break;
                        case 3: y -= distance; break;
                    }
                    
                    if (x < 0 || x >= boardWidth || y < 0 || y >= boardHeight)
                        break;
                    
                    if (board[x, y].Type == TileType.Wall)
                        break;
                    
                    if (board[x, y].IsBreakable())
                    {
                        board[x, y].Break();
                        eventSystem.Notify(GameEventType.WallDestroyed, new GameEventData(x, y));
                        break;
                    }
                    
                    explosions.Add(entityFactory.CreateExplosion(x * tileSize, y * tileSize, 30));
                }
            }
        }
        
        public List<Bomb> GetBombs() => bombs;
        
        public bool IsBombAtPosition(int x, int y)
        {
            foreach (var bomb in bombs)
            {
                if (bomb.X == x && bomb.Y == y)
                    return true;
            }
            return false;
        }
    }
}

