using System;
using System.Collections.Generic;
using BombermanGame.Entities;
using BombermanGame.Factories;
using BombermanGame.Observers;

namespace BombermanGame.Managers
{
    public class PowerUpManager
    {
        private List<PowerUp> powerUps;
        private Random random;
        private GameEventSystem eventSystem;
        
        public PowerUpManager()
        {
            powerUps = new List<PowerUp>();
            random = new Random();
            eventSystem = GameEventSystem.Instance;
        }
        
        public void SpawnPowerUp(int x, int y)
        {
            PowerUpType type = (PowerUpType)random.Next(3);
            powerUps.Add(EntityFactory.CreatePowerUp(x * 32, y * 32, type));
        }
        
        public void Update()
        {
            foreach (var powerUp in powerUps)
            {
                powerUp.Update();
            }
        }
        
        public PowerUp CheckCollision(Player player)
        {
            foreach (var powerUp in powerUps)
            {
                if (Math.Abs(player.X - powerUp.X) < 32 && Math.Abs(player.Y - powerUp.Y) < 32)
                {
                    return powerUp;
                }
            }
            return null;
        }
        
        public void RemovePowerUp(PowerUp powerUp)
        {
            powerUps.Remove(powerUp);
            eventSystem.Notify(GameEventType.PowerUpCollected, new GameEventData(powerUp.X, powerUp.Y, (int)powerUp.Type));
        }
        
        public List<PowerUp> GetPowerUps() => powerUps;
    }
}

