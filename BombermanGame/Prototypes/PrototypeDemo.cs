using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BombermanGame.Entities;

namespace BombermanGame.Prototypes
{
    // PROTOTYPE PATTERN - Deep vs Shallow Copy Demonstration
    
    // Complex entity with reference types for deep/shallow copy comparison
    public class ComplexPlayer : GameEntity
    {
        public int Speed { get; set; }
        public List<string> Inventory { get; set; }  // Reference type for demonstration
        public PlayerStats Stats { get; set; }       // Reference type for demonstration
        
        public ComplexPlayer(int x, int y) : base(x, y, 20)
        {
            Speed = 2;
            Inventory = new List<string>();
            Stats = new PlayerStats();
        }
        
        // SHALLOW COPY - Only copies value types, reference types are shared
        public ComplexPlayer ShallowCopy()
        {
            // MemberwiseClone creates a shallow copy
            return (ComplexPlayer)this.MemberwiseClone();
        }
        
        // DEEP COPY - Creates new instances of all reference types
        public ComplexPlayer DeepCopy()
        {
            var deepCopy = new ComplexPlayer(this.X, this.Y);
            deepCopy.Speed = this.Speed;
            deepCopy.Size = this.Size;
            deepCopy.IsActive = this.IsActive;
            
            // Create NEW instances of reference types
            deepCopy.Inventory = new List<string>(this.Inventory);
            deepCopy.Stats = new PlayerStats
            {
                Health = this.Stats.Health,
                Score = this.Stats.Score,
                Level = this.Stats.Level
            };
            
            return deepCopy;
        }
        
        public override void Update() { }
        
        public override void Render(System.Drawing.Graphics graphics) { }
        
        public override GameEntity Clone()
        {
            return DeepCopy();
        }
    }
    
    // Reference type class for demonstration
    public class PlayerStats
    {
        public int Health { get; set; } = 100;
        public int Score { get; set; } = 0;
        public int Level { get; set; } = 1;
    }
    
    // Demonstration class that shows the difference between deep and shallow copies
    public class PrototypeCopyComparison
    {
        public static void DemonstrateShallowVsDeepCopy()
        {
            Console.WriteLine("=== PROTOTYPE PATTERN: Deep vs Shallow Copy Demonstration ===\n");
            
            // Create original object
            ComplexPlayer original = new ComplexPlayer(100, 100);
            original.Speed = 5;
            original.Inventory.Add("Bomb");
            original.Inventory.Add("PowerUp");
            original.Stats.Health = 80;
            original.Stats.Score = 500;
            
            Console.WriteLine("ORIGINAL OBJECT:");
            PrintObjectInfo(original, "Original");
            
            // SHALLOW COPY
            Console.WriteLine("\n--- SHALLOW COPY ---");
            ComplexPlayer shallowCopy = original.ShallowCopy();
            Console.WriteLine("After creating shallow copy:");
            PrintObjectInfo(shallowCopy, "ShallowCopy");
            
            Console.WriteLine("\nModifying original's inventory (adding 'Key')...");
            original.Inventory.Add("Key");
            
            Console.WriteLine("\nAfter modification:");
            Console.WriteLine("Original inventory: " + string.Join(", ", original.Inventory));
            Console.WriteLine("ShallowCopy inventory: " + string.Join(", ", shallowCopy.Inventory));
            Console.WriteLine("⚠️ NOTICE: ShallowCopy's inventory also changed! (Shared reference)");
            
            Console.WriteLine("\nModifying original's Stats.Score to 1000...");
            original.Stats.Score = 1000;
            Console.WriteLine("Original Stats.Score: " + original.Stats.Score);
            Console.WriteLine("ShallowCopy Stats.Score: " + shallowCopy.Stats.Score);
            Console.WriteLine("⚠️ NOTICE: ShallowCopy's Stats also changed! (Shared reference)");
            
            // DEEP COPY
            Console.WriteLine("\n\n--- DEEP COPY ---");
            ComplexPlayer original2 = new ComplexPlayer(200, 200);
            original2.Speed = 5;
            original2.Inventory.Add("Bomb");
            original2.Inventory.Add("PowerUp");
            original2.Stats.Health = 80;
            original2.Stats.Score = 500;
            
            ComplexPlayer deepCopy = original2.DeepCopy();
            Console.WriteLine("After creating deep copy:");
            PrintObjectInfo(deepCopy, "DeepCopy");
            
            Console.WriteLine("\nModifying original2's inventory (adding 'Key')...");
            original2.Inventory.Add("Key");
            
            Console.WriteLine("\nAfter modification:");
            Console.WriteLine("Original2 inventory: " + string.Join(", ", original2.Inventory));
            Console.WriteLine("DeepCopy inventory: " + string.Join(", ", deepCopy.Inventory));
            Console.WriteLine("✓ NOTICE: DeepCopy's inventory is independent! (Separate reference)");
            
            Console.WriteLine("\nModifying original2's Stats.Score to 1000...");
            original2.Stats.Score = 1000;
            Console.WriteLine("Original2 Stats.Score: " + original2.Stats.Score);
            Console.WriteLine("DeepCopy Stats.Score: " + deepCopy.Stats.Score);
            Console.WriteLine("✓ NOTICE: DeepCopy's Stats is independent! (Separate reference)");
            
            // MEMORY ADDRESS COMPARISON
            Console.WriteLine("\n\n=== MEMORY ADDRESS COMPARISON ===");
            CompareMemoryAddresses(original, shallowCopy, deepCopy);
        }
        
        private static void PrintObjectInfo(ComplexPlayer player, string name)
        {
            Console.WriteLine($"{name}:");
            Console.WriteLine($"  Position: ({player.X}, {player.Y})");
            Console.WriteLine($"  Speed: {player.Speed}");
            Console.WriteLine($"  Inventory: {string.Join(", ", player.Inventory)}");
            Console.WriteLine($"  Stats - Health: {player.Stats.Health}, Score: {player.Stats.Score}");
            Console.WriteLine($"  Object HashCode: {player.GetHashCode()}");
            Console.WriteLine($"  Inventory HashCode: {player.Inventory.GetHashCode()}");
            Console.WriteLine($"  Stats HashCode: {player.Stats.GetHashCode()}");
        }
        
        private static void CompareMemoryAddresses(ComplexPlayer original, ComplexPlayer shallowCopy, ComplexPlayer deepCopy)
        {
            Console.WriteLine("\nOBJECT ADDRESSES:");
            Console.WriteLine($"Original Object:     HashCode = {original.GetHashCode()}, RuntimeHandle = {RuntimeHelpers.GetHashCode(original)}");
            Console.WriteLine($"ShallowCopy Object:  HashCode = {shallowCopy.GetHashCode()}, RuntimeHandle = {RuntimeHelpers.GetHashCode(shallowCopy)}");
            Console.WriteLine($"DeepCopy Object:     HashCode = {deepCopy.GetHashCode()}, RuntimeHandle = {RuntimeHelpers.GetHashCode(deepCopy)}");
            
            Console.WriteLine("\nINVENTORY LIST ADDRESSES:");
            Console.WriteLine($"Original.Inventory:     HashCode = {original.Inventory.GetHashCode()}, RuntimeHandle = {RuntimeHelpers.GetHashCode(original.Inventory)}");
            Console.WriteLine($"ShallowCopy.Inventory:  HashCode = {shallowCopy.Inventory.GetHashCode()}, RuntimeHandle = {RuntimeHelpers.GetHashCode(shallowCopy.Inventory)}");
            Console.WriteLine($"DeepCopy.Inventory:     HashCode = {deepCopy.Inventory.GetHashCode()}, RuntimeHandle = {RuntimeHelpers.GetHashCode(deepCopy.Inventory)}");
            
            bool shallowInventorySame = RuntimeHelpers.GetHashCode(original.Inventory) == RuntimeHelpers.GetHashCode(shallowCopy.Inventory);
            bool deepInventorySame = RuntimeHelpers.GetHashCode(original.Inventory) == RuntimeHelpers.GetHashCode(deepCopy.Inventory);
            
            Console.WriteLine($"\n✓ Original and ShallowCopy share Inventory? {shallowInventorySame}");
            Console.WriteLine($"✓ Original and DeepCopy share Inventory? {deepInventorySame}");
            
            Console.WriteLine("\nSTATS OBJECT ADDRESSES:");
            Console.WriteLine($"Original.Stats:     HashCode = {original.Stats.GetHashCode()}, RuntimeHandle = {RuntimeHelpers.GetHashCode(original.Stats)}");
            Console.WriteLine($"ShallowCopy.Stats:  HashCode = {shallowCopy.Stats.GetHashCode()}, RuntimeHandle = {RuntimeHelpers.GetHashCode(shallowCopy.Stats)}");
            Console.WriteLine($"DeepCopy.Stats:     HashCode = {deepCopy.Stats.GetHashCode()}, RuntimeHandle = {RuntimeHelpers.GetHashCode(deepCopy.Stats)}");
            
            bool shallowStatsSame = RuntimeHelpers.GetHashCode(original.Stats) == RuntimeHelpers.GetHashCode(shallowCopy.Stats);
            bool deepStatsSame = RuntimeHelpers.GetHashCode(original.Stats) == RuntimeHelpers.GetHashCode(deepCopy.Stats);
            
            Console.WriteLine($"\n✓ Original and ShallowCopy share Stats? {shallowStatsSame}");
            Console.WriteLine($"✓ Original and DeepCopy share Stats? {deepStatsSame}");
            
            Console.WriteLine("\n=== SUMMARY ===");
            Console.WriteLine("SHALLOW COPY:");
            Console.WriteLine("  - Creates a new object instance");
            Console.WriteLine("  - Copies value types (int, bool, etc.)");
            Console.WriteLine("  - Reference types (List, objects) are SHARED with original");
            Console.WriteLine("  - Changes to reference types affect both original and copy");
            Console.WriteLine("  - Faster and uses less memory");
            
            Console.WriteLine("\nDEEP COPY:");
            Console.WriteLine("  - Creates a new object instance");
            Console.WriteLine("  - Copies value types (int, bool, etc.)");
            Console.WriteLine("  - Reference types (List, objects) are CLONED to new instances");
            Console.WriteLine("  - Changes to reference types do NOT affect the other copy");
            Console.WriteLine("  - Slower and uses more memory, but completely independent");
        }
    }
}

