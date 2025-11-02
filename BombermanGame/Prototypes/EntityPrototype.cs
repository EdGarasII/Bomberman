using System;
using System.Collections.Generic;
using BombermanGame.Entities;

namespace BombermanGame.Prototypes
{
    // PROTOTYPE PATTERN - Registry of prototype entities for cloning
    public class EntityPrototype
    {
        private static Dictionary<string, GameEntity> prototypes = new Dictionary<string, GameEntity>();
        
        public static void RegisterPrototype(string key, GameEntity prototype)
        {
            prototypes[key] = prototype;
        }
        
        public static GameEntity Clone(string key)
        {
            if (prototypes.ContainsKey(key))
            {
                return prototypes[key].Clone();
            }
            return null;
        }
        
        public static GameEntity GetPrototype(string key)
        {
            if (prototypes.ContainsKey(key))
            {
                return prototypes[key];
            }
            return null;
        }
        
        public static bool HasPrototype(string key)
        {
            return prototypes.ContainsKey(key);
        }
        
        public static void RemovePrototype(string key)
        {
            prototypes.Remove(key);
        }
        
        public static void ClearPrototypes()
        {
            prototypes.Clear();
        }
    }
}

