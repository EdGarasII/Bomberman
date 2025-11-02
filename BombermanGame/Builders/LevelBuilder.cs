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
        private bool useDeterministicPattern = false;
        
        public LevelBuilder(int width, int height, AbstractEntityFactory factory, int? seed = null)
        {
            this.width = width;
            this.height = height;
            this.entityFactory = factory;
            this.random = seed.HasValue ? new Random(seed.Value) : new Random();
            this.board = new Tile[width, height];
        }
        
        public LevelBuilder WithDeterministicPattern(bool useDeterministic = true)
        {
            useDeterministicPattern = useDeterministic;
            return this;
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
                    bool shouldPlaceWall;
                    
                    if (useDeterministicPattern)
                    {
                        // Deterministic pattern: place wall if (x+y) % 3 != 0
                        // This ensures all clients generate the same map for multiplayer
                        shouldPlaceWall = (x + y) % 3 != 0;
                    }
                    else
                    {
                        // Random pattern
                        shouldPlaceWall = random.NextDouble() < density;
                    }
                    
                    if (shouldPlaceWall)
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
        
        public LevelBuilder WithClearStartingPositions(int[,] positions, int radius = 1)
        {
            for (int i = 0; i < positions.GetLength(0); i++)
            {
                int x = positions[i, 0];
                int y = positions[i, 1];
                WithClearStartingPosition(x, y, radius);
            }
            return this;
        }
        
        public Tile[,] Build()
        {
            return board;
        }
    }
}

