using System;
using BombermanGame.Entities;

namespace BombermanGame.Iterators
{
    // ITERATOR PATTERN - Tile collection using 2D array
    public class TileGrid : IIterable<Tile>
    {
        private Tile[,] tiles;
        private int width;
        private int height;
        
        public TileGrid(int width, int height)
        {
            this.width = width;
            this.height = height;
            tiles = new Tile[width, height];
        }
        
        public void SetTile(int x, int y, Tile tile)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                tiles[x, y] = tile;
            }
        }
        
        public Tile? GetTile(int x, int y)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                return tiles[x, y];
            }
            return null;
        }
        
        public int Width => width;
        public int Height => height;
        
        public IIterator<Tile> CreateIterator()
        {
            return new TileIterator(this);
        }
    }
    
    // ITERATOR PATTERN - Iterator for Tile collection (2D Array-based)
    public class TileIterator : IIterator<Tile>
    {
        private TileGrid grid;
        private int currentX;
        private int currentY;
        
        public TileIterator(TileGrid grid)
        {
            this.grid = grid;
            currentX = 0;
            currentY = 0;
        }
        
        public bool HasNext()
        {
            // Skip null tiles
            while (currentY < grid.Height)
            {
                if (currentX < grid.Width)
                {
                    if (grid.GetTile(currentX, currentY) != null)
                        return true;
                    currentX++;
                }
                else
                {
                    currentX = 0;
                    currentY++;
                }
            }
            return false;
        }
        
        public Tile Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("No more elements");
            
            Tile tile = grid.GetTile(currentX, currentY);
            currentX++;
            
            if (currentX >= grid.Width)
            {
                currentX = 0;
                currentY++;
            }
            
            return tile;
        }
        
        public void Reset()
        {
            currentX = 0;
            currentY = 0;
        }
    }
}

