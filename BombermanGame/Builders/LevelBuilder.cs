using System;
using BombermanGame.Entities;
using BombermanGame.Factories;

namespace BombermanGame.Builders
{
    // BUILDER PATTERN - Constructs game levels step by step
    public class LevelBuilder
    {
        private Tile[,] board;
        private int width;
        private int height;
        private AbstractEntityFactory entityFactory;
        private Random random;
        
        public LevelBuilder(int width, int height, AbstractEntityFactory factory)
        {
            this.width = width;
            this.height = height;
            this.entityFactory = factory;
            this.random = new Random();
            this.board = new Tile[width, height];
        }
        
        public LevelBuilder WithEmptyTiles()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    board[x, y] = entityFactory.CreateTile(x, y, TileType.Empty);
                }
            }
            return this;
        }
        
        public LevelBuilder WithBorderWalls()
        {
            for (int x = 0; x < width; x++)
            {
                board[x, 0] = entityFactory.CreateTile(x, 0, TileType.Wall);
                board[x, height - 1] = entityFactory.CreateTile(x, height - 1, TileType.Wall);
            }
            for (int y = 0; y < height; y++)
            {
                board[0, y] = entityFactory.CreateTile(0, y, TileType.Wall);
                board[width - 1, y] = entityFactory.CreateTile(width - 1, y, TileType.Wall);
            }
            return this;
        }
        
        public LevelBuilder WithBreakableWalls(double density = 0.7)
        {
            for (int x = 2; x < width - 2; x += 2)
            {
                for (int y = 2; y < height - 2; y += 2)
                {
                    if (random.NextDouble() < density)
                    {
                        board[x, y] = entityFactory.CreateTile(x, y, TileType.BreakableWall);
                    }
                }
            }
            return this;
        }
        
        public LevelBuilder WithClearStartingPosition(int x, int y, int radius = 1)
        {
            for (int dx = -radius; dx <= radius; dx++)
            {
                for (int dy = -radius; dy <= radius; dy++)
                {
                    int posX = x + dx;
                    int posY = y + dy;
                    if (posX >= 0 && posX < width && posY >= 0 && posY < height)
                    {
                        board[posX, posY] = entityFactory.CreateTile(posX, posY, TileType.Empty);
                    }
                }
            }
            return this;
        }
        
        public Tile[,] Build()
        {
            return board;
        }
    }
}

