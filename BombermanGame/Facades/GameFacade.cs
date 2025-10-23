using System;
using System.Collections.Generic;
using System.Drawing;
using BombermanGame.Entities;
using BombermanGame.Factories;
using BombermanGame.Commands;
using BombermanGame.Observers;
using BombermanGame.Core;

namespace BombermanGame.Facades
{
    // FACADE PATTERN - Simplified interface to complex game systems
    public class GameFacade
    {
        private Player player;
        private List<Bomb> bombs;
        private List<Explosion> explosions;
        private List<Enemy> enemies;
        private List<PowerUp> powerUps;
        private Tile[,] board;
        private CommandInvoker commandInvoker;
        private AbstractEntityFactory entityFactory;
        private GameEventSystem eventSystem;
        
        private const int TILE_SIZE = 32;
        private int boardWidth;
        private int boardHeight;
        
        public GameFacade(int width, int height)
        {
            boardWidth = width;
            boardHeight = height;
            bombs = new List<Bomb>();
            explosions = new List<Explosion>();
            enemies = new List<Enemy>();
            powerUps = new List<PowerUp>();
            commandInvoker = new CommandInvoker();
            entityFactory = new StandardEntityFactory();
            eventSystem = GameEventSystem.Instance;
        }
        
        public void InitializeGame()
        {
            player = EntityFactory.CreatePlayer(100, 100);
            GenerateLevel();
        }
        
        public void GenerateLevel()
        {
            board = new Tile[boardWidth, boardHeight];
            
            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    board[x, y] = entityFactory.CreateTile(x, y, TileType.Empty);
                }
            }
            
            // Add borders and breakable walls...
        }
        
        public void UpdateGame()
        {
            player?.Update();
            UpdateBombs();
            UpdateExplosions();
            UpdateEnemies();
        }
        
        public void MovePlayer(int deltaX, int deltaY)
        {
            if (player == null) return;
            
            var moveCommand = new MoveCommand(player, deltaX, deltaY);
            commandInvoker.ExecuteCommand(moveCommand);
        }
        
        public void PlaceBomb()
        {
            if (player == null || player.BombCount <= 0) return;
            
            int bombX = (player.X + player.Size / 2) / TILE_SIZE * TILE_SIZE;
            int bombY = (player.Y + player.Size / 2) / TILE_SIZE * TILE_SIZE;
            
            bombs.Add(entityFactory.CreateBomb(bombX, bombY, player.BombRange));
            player.PlaceBomb();
        }
        
        private void UpdateBombs()
        {
            for (int i = bombs.Count - 1; i >= 0; i--)
            {
                bombs[i].Update();
                if (bombs[i].ShouldExplode())
                {
                    ExplodeBomb(bombs[i]);
                    bombs.RemoveAt(i);
                }
            }
        }
        
        private void UpdateExplosions()
        {
            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                explosions[i].Update();
                if (explosions[i].IsFinished())
                {
                    explosions.RemoveAt(i);
                }
            }
        }
        
        private void UpdateEnemies()
        {
            foreach (var enemy in enemies)
            {
                enemy.Update();
            }
        }
        
        private void ExplodeBomb(Bomb bomb)
        {
            eventSystem.Notify(GameEventType.BombExploded, new GameEventData(bomb.X, bomb.Y));
        }
        
        public Player GetPlayer() => player;
        public List<Bomb> GetBombs() => bombs;
        public List<Explosion> GetExplosions() => explosions;
        public List<Enemy> GetEnemies() => enemies;
        public List<PowerUp> GetPowerUps() => powerUps;
        public Tile[,] GetBoard() => board;
    }
}

