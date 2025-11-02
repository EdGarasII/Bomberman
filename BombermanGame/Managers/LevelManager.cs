using System;
using BombermanGame.Entities;
using BombermanGame.Factories;
using BombermanGame.Builders;

namespace BombermanGame.Managers
{
    public class LevelManager
    {
        private int currentLevel;
        private AbstractEntityFactory entityFactory;
        
        public LevelManager()
        {
            currentLevel = 1;
            entityFactory = new StandardEntityFactory();
        }
        
        public Tile[,] GenerateLevel(int width, int height, bool useMultiplayerPattern = false, int? seed = null)
        {
            // Use fixed seed for multiplayer so all clients have the same map
            int actualSeed = seed ?? (useMultiplayerPattern ? 12345 : Environment.TickCount);
            
            // Define player starting positions to clear (top-left, top-right, bottom-left, bottom-right corners)
            int[,] startingPositions = new int[,]
            {
                { 1, 1 }, { 2, 1 }, { 1, 2 },  // Top-left corner
                { width - 2, 1 }, { width - 3, 1 }, { width - 2, 2 },  // Top-right corner
                { 1, height - 2 }, { 2, height - 2 }, { 1, height - 3 },  // Bottom-left corner
                { width - 2, height - 2 }, { width - 3, height - 2 }, { width - 2, height - 3 }  // Bottom-right corner
            };
            
            // Use Builder Pattern to construct the level
            LevelBuilder builder = new LevelBuilder(width, height, entityFactory, actualSeed);
            
            return builder
                .WithEmptyTiles()
                .WithBorderWalls()
                .WithDeterministicPattern(useMultiplayerPattern)  // Use deterministic pattern for multiplayer
                .WithBreakableWalls()
                .WithClearStartingPositions(startingPositions)
                .Build();
        }
        
        public void NextLevel()
        {
            currentLevel++;
        }
        
        public int GetCurrentLevel() => currentLevel;
    }
}

