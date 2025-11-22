using System;
using BombermanGame.Entities;

namespace BombermanGame.Bridges
{
    // BRIDGE PATTERN - Separates power-up application abstraction from effect implementation
    // Allows power-up application strategies and effect implementations to vary independently
    
    // Implementor interface - defines the power-up effect implementation interface
    public interface IPowerUpEffect
    {
        void ApplyEffect(Player player, PowerUpType type);
        void RemoveEffect(Player player, PowerUpType type);
        bool IsEffectActive(Player player, PowerUpType type);
    }
    
    public class DirectModificationEffect : IPowerUpEffect
    {
        public void ApplyEffect(Player player, PowerUpType type)
        {
            switch (type)
            {
                case PowerUpType.Speed:
                    player.Speed += 1;
                    break;
                case PowerUpType.BombCount:
                    player.MaxBombs++;
                    player.BombCount++;
                    break;
                case PowerUpType.BombRange:
                    player.BombRange++;
                    break;
            }
        }
        
        public void RemoveEffect(Player player, PowerUpType type)
        {
            switch (type)
            {
                case PowerUpType.Speed:
                    player.Speed = Math.Max(1, player.Speed - 1);
                    break;
                case PowerUpType.BombCount:
                    player.MaxBombs = Math.Max(1, player.MaxBombs - 1);
                    if (player.BombCount > player.MaxBombs)
                    {
                        player.BombCount = player.MaxBombs;
                    }
                    break;
                case PowerUpType.BombRange:
                    player.BombRange = Math.Max(1, player.BombRange - 1);
                    break;
            }
        }
        
        public bool IsEffectActive(Player player, PowerUpType type)
        {
            return true;
        }
    }
    
    public class BuffBasedEffect : IPowerUpEffect
    {
        private Dictionary<Player, Dictionary<PowerUpType, int>> playerBuffs;
        
        public BuffBasedEffect()
        {
            playerBuffs = new Dictionary<Player, Dictionary<PowerUpType, int>>();
        }
        
        public void ApplyEffect(Player player, PowerUpType type)
        {
            if (!playerBuffs.ContainsKey(player))
            {
                playerBuffs[player] = new Dictionary<PowerUpType, int>();
            }
            
            if (!playerBuffs[player].ContainsKey(type))
            {
                playerBuffs[player][type] = 0;
            }
            
            playerBuffs[player][type]++;
            
            switch (type)
            {
                case PowerUpType.Speed:
                    player.Speed += 1;
                    break;
                case PowerUpType.BombCount:
                    player.MaxBombs++;
                    player.BombCount++;
                    break;
                case PowerUpType.BombRange:
                    player.BombRange++;
                    break;
            }
        }
        
        public void RemoveEffect(Player player, PowerUpType type)
        {
            if (!playerBuffs.ContainsKey(player) || !playerBuffs[player].ContainsKey(type))
                return;
            
            if (playerBuffs[player][type] > 0)
            {
                playerBuffs[player][type]--;
                
                switch (type)
                {
                    case PowerUpType.Speed:
                        player.Speed = Math.Max(1, player.Speed - 1);
                        break;
                    case PowerUpType.BombCount:
                        player.MaxBombs = Math.Max(1, player.MaxBombs - 1);
                        if (player.BombCount > player.MaxBombs)
                        {
                            player.BombCount = player.MaxBombs;
                        }
                        break;
                    case PowerUpType.BombRange:
                        player.BombRange = Math.Max(1, player.BombRange - 1);
                        break;
                }
                
                if (playerBuffs[player][type] == 0)
                {
                    playerBuffs[player].Remove(type);
                }
            }
        }
        
        public bool IsEffectActive(Player player, PowerUpType type)
        {
            if (!playerBuffs.ContainsKey(player) || !playerBuffs[player].ContainsKey(type))
                return false;
            
            return playerBuffs[player][type] > 0;
        }
        
        public int GetBuffCount(Player player, PowerUpType type)
        {
            if (!playerBuffs.ContainsKey(player) || !playerBuffs[player].ContainsKey(type))
                return 0;
            
            return playerBuffs[player][type];
        }
    }
    
    // Abstraction - Base power-up applicator
    public abstract class PowerUpApplicator
    {
        protected IPowerUpEffect powerUpEffect;
        
        protected PowerUpApplicator(IPowerUpEffect effect)
        {
            powerUpEffect = effect ?? throw new ArgumentNullException(nameof(effect));
        }
        
        public abstract void ApplyPowerUp(Player player, PowerUpType type);
        public abstract void RemovePowerUp(Player player, PowerUpType type);
        public abstract bool HasPowerUp(Player player, PowerUpType type);
    }
    
    // Refined Abstraction 1: Immediate applicator
    public class ImmediatePowerUpApplicator : PowerUpApplicator
    {
        public ImmediatePowerUpApplicator(IPowerUpEffect effect) : base(effect)
        {
        }
        
        public override void ApplyPowerUp(Player player, PowerUpType type)
        {
            if (player == null) return;
            powerUpEffect.ApplyEffect(player, type);
        }
        
        public override void RemovePowerUp(Player player, PowerUpType type)
        {
            if (player == null) return;
            powerUpEffect.RemoveEffect(player, type);
        }
        
        public override bool HasPowerUp(Player player, PowerUpType type)
        {
            if (player == null) return false;
            return powerUpEffect.IsEffectActive(player, type);
        }
    }
    
    // Refined Abstraction 2: Validated applicator
    public class ValidatedPowerUpApplicator : PowerUpApplicator
    {
        private HashSet<Tuple<Player, PowerUpType>> appliedPowerUps;
        
        public ValidatedPowerUpApplicator(IPowerUpEffect effect) : base(effect)
        {
            appliedPowerUps = new HashSet<Tuple<Player, PowerUpType>>();
        }
        
        public override void ApplyPowerUp(Player player, PowerUpType type)
        {
            if (player == null) return;
            
            // Validation for power-up application - allow stacking if under limit
            if (CanApplyPowerUp(player, type))
            {
                powerUpEffect.ApplyEffect(player, type);
                // Track that this power-up was applied (for removal later if needed)
                var key = Tuple.Create(player, type);
                if (!appliedPowerUps.Contains(key))
                {
                    appliedPowerUps.Add(key);
                }
            }
        }
        
        public override void RemovePowerUp(Player player, PowerUpType type)
        {
            if (player == null) return;
            
            var key = Tuple.Create(player, type);
            if (appliedPowerUps.Contains(key))
            {
                powerUpEffect.RemoveEffect(player, type);
                appliedPowerUps.Remove(key);
            }
        }
        
        public override bool HasPowerUp(Player player, PowerUpType type)
        {
            if (player == null) return false;
            
            var key = Tuple.Create(player, type);
            return appliedPowerUps.Contains(key) && powerUpEffect.IsEffectActive(player, type);
        }
        
        private bool CanApplyPowerUp(Player player, PowerUpType type)
        {
            // Validation logic to prevent excessive power-ups
            switch (type)
            {
                case PowerUpType.Speed:
                    return player.Speed < 10;
                case PowerUpType.BombCount:
                    return player.MaxBombs < 10;
                case PowerUpType.BombRange:
                    return player.BombRange < 10;
                default:
                    return true;
            }
        }
    }
}

