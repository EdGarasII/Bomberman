using System;
using System.Runtime.CompilerServices;
using BombermanGame.Entities;
using BombermanGame.Patterns;

namespace BombermanGame.Prototypes
{
    // PROTOTYPE PATTERN - Simple demonstration of shallow vs deep copy
    public class EntityPrototype
    {
        public static void DemonstrateShallowVsDeepCopy(Enemy? original = null)
        {
            if (original == null)
            {
                original = new Enemy(0, 0, new BasicAIStrategy());
            }
            
            Console.WriteLine("\n--- SHALLOW COPY ---\n");
            Console.WriteLine($"Original object: {RuntimeHelpers.GetHashCode(original)}\n");
            
            var shallowCopy = original.Clone();
            Console.WriteLine($"Shallow copy object: {RuntimeHelpers.GetHashCode(shallowCopy)}\n");
            Console.WriteLine($"Original.Speed: {original.Speed}\n");
            Console.WriteLine($"ShallowCopy.Speed: {shallowCopy.Speed}\n");
            Console.WriteLine($"Original.AIStrategy: {RuntimeHelpers.GetHashCode(original.AIStrategy)}\n");
            Console.WriteLine($"ShallowCopy.AIStrategy: {RuntimeHelpers.GetHashCode(shallowCopy.AIStrategy)}\n");
            
            Console.WriteLine("--- DEEP COPY ---\n");
            Console.WriteLine($"Original object: {RuntimeHelpers.GetHashCode(original)}\n");
            
            var deepCopy = original.DeepClone();
            Console.WriteLine($"Deep copy object: {RuntimeHelpers.GetHashCode(deepCopy)}\n");
            Console.WriteLine($"Original.Speed: {original.Speed}\n");
            Console.WriteLine($"DeepCopy.Speed: {deepCopy.Speed}\n");
            Console.WriteLine($"Original.AIStrategy: {RuntimeHelpers.GetHashCode(original.AIStrategy)}\n");
            Console.WriteLine($"DeepCopy.AIStrategy: {RuntimeHelpers.GetHashCode(deepCopy.AIStrategy)}\n");
        }
    }
}
