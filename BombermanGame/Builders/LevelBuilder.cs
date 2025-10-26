using System;
using BombermanGame.Entities;
using BombermanGame.Factories;

namespace BombermanGame.Builders
{
    // BUILDER PATTERN - Abstract builder interface
    public abstract class ILevelBuilder
    {
        protected Tile[,] board;
        protected int width;
        protected int height;
        protected AbstractEntityFactory entityFactory;
        protected Random random;
        
        public ILevelBuilder(int width, int height, AbstractEntityFactory factory)
        {
            this.width = width;
            this.height = height;
            this.entityFactory = factory;
            this.random = new Random();
            this.board = new Tile[width, height];
        }
        
        public abstract ILevelBuilder BuildEmptyTiles();
        public abstract ILevelBuilder BuildBorderWalls();
        public abstract ILevelBuilder BuildBreakableWalls();
        public abstract ILevelBuilder BuildClearStartingPosition();
        
        public Tile[,] GetResult()
        {
            return board;
        }
    }
    
    // CONCRETE BUILDER 1 - Easy level builder
    public class EasyLevelBuilder : ILevelBuilder
    {
        public EasyLevelBuilder(int width, int height, AbstractEntityFactory factory) 
            : base(width, height, factory)
        {
        }
        
        public override ILevelBuilder BuildEmptyTiles()
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
        
        public override ILevelBuilder BuildBorderWalls()
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
        
        public override ILevelBuilder BuildBreakableWalls()
        {
            // Easy: Low density of walls (30%)
            double density = 0.3;
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
        
        public override ILevelBuilder BuildClearStartingPosition()
        {
            // Easy: Large clear area (radius 2)
            int x = 1, y = 1;
            int radius = 2;
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
    }
    
    // CONCRETE BUILDER 2 - Hard level builder
    public class HardLevelBuilder : ILevelBuilder
    {
        public HardLevelBuilder(int width, int height, AbstractEntityFactory factory) 
            : base(width, height, factory)
        {
        }
        
        public override ILevelBuilder BuildEmptyTiles()
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
        
        public override ILevelBuilder BuildBorderWalls()
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
            
            // Hard: Add extra internal walls
            for (int x = 2; x < width - 2; x += 2)
            {
                for (int y = 2; y < height - 2; y += 2)
                {
                    board[x, y] = entityFactory.CreateTile(x, y, TileType.Wall);
                }
            }
            return this;
        }
        
        public override ILevelBuilder BuildBreakableWalls()
        {
            // Hard: High density of walls (80%)
            double density = 0.8;
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    if (board[x, y].Type == TileType.Empty && random.NextDouble() < density)
                    {
                        board[x, y] = entityFactory.CreateTile(x, y, TileType.BreakableWall);
                    }
                }
            }
            return this;
        }
        
        public override ILevelBuilder BuildClearStartingPosition()
        {
            // Hard: Small clear area (radius 1)
            int x = 1, y = 1;
            int radius = 1;
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
    }
    
    // Director class to demonstrate builder usage
    public class LevelDirector
    {
        public Tile[,] ConstructEasyLevel(ILevelBuilder builder)
        {
            return builder.BuildEmptyTiles()
                         .BuildBorderWalls()
                         .BuildBreakableWalls()
                         .BuildClearStartingPosition()
                         .GetResult();
        }
        
        public Tile[,] ConstructHardLevel(ILevelBuilder builder)
        {
            return builder.BuildEmptyTiles()
                         .BuildBorderWalls()
                         .BuildBreakableWalls()
                         .BuildClearStartingPosition()
                         .GetResult();
        }
    }
}

