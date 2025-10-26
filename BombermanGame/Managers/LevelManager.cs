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
        private LevelDirector director;
        
        public LevelManager()
        {
            currentLevel = 1;
            entityFactory = new StandardEntityFactory();
            director = new LevelDirector();
        }
        
        public Tile[,] GenerateLevel(int width, int height)
        {
            // Use EasyLevelBuilder for first few levels, then HardLevelBuilder
            ILevelBuilder builder;
            
            if (currentLevel <= 3)
            {
                builder = new EasyLevelBuilder(width, height, entityFactory);
            }
            else
            {
                builder = new HardLevelBuilder(width, height, entityFactory);
            }
            
            return builder
                .BuildEmptyTiles()
                .BuildBorderWalls()
                .BuildBreakableWalls()
                .BuildClearStartingPosition()
                .GetResult();
        }
        
        public void NextLevel()
        {
            currentLevel++;
        }
        
        public int GetCurrentLevel() => currentLevel;
    }
}

