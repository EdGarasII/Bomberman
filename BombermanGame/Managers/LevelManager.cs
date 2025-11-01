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
        
        public Tile[,] GenerateLevel(int width, int height)
        {
            var builder = new LevelBuilder(width, height, entityFactory);
            
            return builder
                .WithEmptyTiles()
                .WithBorderWalls()
                .WithBreakableWalls(0.7)
                .WithClearStartingPosition(1, 1, 1)
                .Build();
        }
        
        public void NextLevel()
        {
            currentLevel++;
        }
        
        public int GetCurrentLevel() => currentLevel;
    }
}

