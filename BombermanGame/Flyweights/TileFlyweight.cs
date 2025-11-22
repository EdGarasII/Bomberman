using System;
using System.Collections.Generic;
using System.Diagnostics;
using BombermanGame.Entities;

namespace BombermanGame.Flyweights
{
    // FLYWEIGHT PATTERN - Intrinsic state (shared)
    public class TileFlyweight
    {
        public TileType Type { get; }
        public string Color { get; }
        public bool IsPassable { get; }
        public bool IsBreakable { get; }
        
        public TileFlyweight(TileType type, string color, bool isPassable, bool isBreakable)
        {
            Type = type;
            Color = color;
            IsPassable = isPassable;
            IsBreakable = isBreakable;
        }
    }
    
    // FLYWEIGHT PATTERN - Flyweight Factory
    public class TileFlyweightFactory
    {
        private static Dictionary<TileType, TileFlyweight> flyweights = new Dictionary<TileType, TileFlyweight>();
        
        public static TileFlyweight GetFlyweight(TileType type)
        {
            if (!flyweights.ContainsKey(type))
            {
                flyweights[type] = CreateFlyweight(type);
            }
            return flyweights[type];
        }
        
        private static TileFlyweight CreateFlyweight(TileType type)
        {
            return type switch
            {
                TileType.Empty => new TileFlyweight(type, "LightGray", true, false),
                TileType.Wall => new TileFlyweight(type, "DarkGray", false, false),
                TileType.BreakableWall => new TileFlyweight(type, "Brown", false, true),
                _ => new TileFlyweight(type, "LightGray", true, false)
            };
        }
        
        public static int GetFlyweightCount()
        {
            return flyweights.Count;
        }
    }
    
    // FLYWEIGHT PATTERN - Context with extrinsic state
    public class TileContext
    {
        private TileFlyweight flyweight;
        public int X { get; }
        public int Y { get; }
        
        public TileContext(int x, int y, TileType type)
        {
            X = x;
            Y = y;
            flyweight = TileFlyweightFactory.GetFlyweight(type);
        }
        
        public TileType Type => flyweight.Type;
        public string Color => flyweight.Color;
        public bool IsPassable => flyweight.IsPassable;
        public bool IsBreakable => flyweight.IsBreakable;
    }
    
    // FLYWEIGHT PATTERN - Performance and memory measurement
    public class FlyweightPerformanceTest
    {
        public static void RunPerformanceTest()
        {
            Console.WriteLine("=== FLYWEIGHT PATTERN PERFORMANCE TEST ===\n");
            
            const int tileCount = 10000;
            
            // Test WITHOUT Flyweight (creating full Tile objects)
            Console.WriteLine("--- Test WITHOUT Flyweight (Full Objects) ---");
            var stopwatch1 = Stopwatch.StartNew();
            var memoryBefore1 = GC.GetTotalMemory(false);
            
            var tilesWithoutFlyweight = new List<Tile>();
            for (int i = 0; i < tileCount; i++)
            {
                var type = (TileType)(i % 3);
                tilesWithoutFlyweight.Add(new Tile(i % 100, i / 100, type));
            }
            
            stopwatch1.Stop();
            var memoryAfter1 = GC.GetTotalMemory(false);
            var memoryUsed1 = memoryAfter1 - memoryBefore1;
            
            Console.WriteLine($"Time: {stopwatch1.ElapsedMilliseconds} ms");
            Console.WriteLine($"Memory used: {memoryUsed1 / 1024} KB");
            Console.WriteLine($"Objects created: {tileCount}\n");
            
            // Test WITH Flyweight (using shared state)
            Console.WriteLine("--- Test WITH Flyweight (Shared State) ---");
            var stopwatch2 = Stopwatch.StartNew();
            var memoryBefore2 = GC.GetTotalMemory(false);
            
            var tilesWithFlyweight = new List<TileContext>();
            for (int i = 0; i < tileCount; i++)
            {
                var type = (TileType)(i % 3);
                tilesWithFlyweight.Add(new TileContext(i % 100, i / 100, type));
            }
            
            stopwatch2.Stop();
            var memoryAfter2 = GC.GetTotalMemory(false);
            var memoryUsed2 = memoryAfter2 - memoryBefore2;
            
            Console.WriteLine($"Time: {stopwatch2.ElapsedMilliseconds} ms");
            Console.WriteLine($"Memory used: {memoryUsed2 / 1024} KB");
            Console.WriteLine($"Objects created: {tileCount}");
            Console.WriteLine($"Flyweight objects: {TileFlyweightFactory.GetFlyweightCount()}\n");
            
            // Results comparison
            Console.WriteLine("--- Performance Comparison ---");
            var timeImprovement = ((double)(stopwatch1.ElapsedMilliseconds - stopwatch2.ElapsedMilliseconds) / stopwatch1.ElapsedMilliseconds) * 100;
            var memoryImprovement = ((double)(memoryUsed1 - memoryUsed2) / memoryUsed1) * 100;
            
            Console.WriteLine($"Time improvement: {timeImprovement:F2}%");
            Console.WriteLine($"Memory improvement: {memoryImprovement:F2}%");
            Console.WriteLine($"Memory saved: {(memoryUsed1 - memoryUsed2) / 1024} KB");
        }
    }
}

