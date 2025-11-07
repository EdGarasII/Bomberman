using System;
using System.Runtime.CompilerServices;
using BombermanGame.Entities;

namespace BombermanGame.Prototypes
{
    // PROTOTYPE PATTERN - Simple demonstration of shallow vs deep copy
    public class EntityPrototype
    {
        // PROTOTYPE PATTERN - Demonstration of shallow vs deep copy with memory addresses
        public static void DemonstrateShallowVsDeepCopy(GameEntity original)
        {
            if (original == null)
            {
                Console.WriteLine("Cannot demonstrate: original is null.");
                return;
            }
            
            Console.WriteLine("\n=== PROTOTYPE PATTERN: Shallow vs Deep Copy Demonstration ===");
            Console.WriteLine($"Original object hash: {RuntimeHelpers.GetHashCode(original)}");
            
            // Shallow copy (Clone method - uses MemberwiseClone)
            var shallowCopy = original.Clone();
            if (shallowCopy != null)
            {
                Console.WriteLine($"\n--- SHALLOW COPY (Clone() - MemberwiseClone) ---");
                Console.WriteLine($"Shallow copy object hash: {RuntimeHelpers.GetHashCode(shallowCopy)}");
                Console.WriteLine($"Original object hash: {RuntimeHelpers.GetHashCode(original)}");
                Console.WriteLine($"Different memory addresses: {RuntimeHelpers.GetHashCode(original) != RuntimeHelpers.GetHashCode(shallowCopy)}");
                Console.WriteLine($"Original X: {original.X}, Shallow Copy X: {shallowCopy.X}");
                
                // Modify shallow copy - for value types, original is NOT affected (MemberwiseClone copies values)
                // But for reference types, they would share references
                int originalX = original.X;
                shallowCopy.X = 888;
                Console.WriteLine($"After modifying shallow copy X to 888:");
                Console.WriteLine($"Original X: {original.X} (unchanged - value type)");
                Console.WriteLine($"Shallow Copy X: {shallowCopy.X} (modified)");
                Console.WriteLine($"Memory addresses are different: {RuntimeHelpers.GetHashCode(original) != RuntimeHelpers.GetHashCode(shallowCopy)}");
                
                // Restore original
                original.X = originalX;
            }
            
            // Deep copy (DeepClone method)
            var deepCopy = original.DeepClone();
            if (deepCopy != null)
            {
                Console.WriteLine($"\n--- DEEP COPY (DeepClone()) ---");
                Console.WriteLine($"Deep copy object hash: {RuntimeHelpers.GetHashCode(deepCopy)}");
                Console.WriteLine($"Original X: {original.X}, Deep Copy X: {deepCopy.X}");
                
                // Modify deep copy - original should not be affected
                deepCopy.X = 999;
                Console.WriteLine($"After modifying deep copy X to 999:");
                Console.WriteLine($"Original X: {original.X} (unchanged)");
                Console.WriteLine($"Deep Copy X: {deepCopy.X} (modified)");
                Console.WriteLine($"Memory addresses are different: {RuntimeHelpers.GetHashCode(original) != RuntimeHelpers.GetHashCode(deepCopy)}");
            }
            
            Console.WriteLine("=== End Demonstration ===\n");
        }
    }
}

