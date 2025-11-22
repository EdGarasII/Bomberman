using System;
using BombermanGame.Entities;
using BombermanGame.Patterns;

namespace BombermanGame.Iterators
{
    // ITERATOR PATTERN - Demonstration class
    public class IteratorPatternDemo
    {
        public static void DemonstrateIteratorPattern()
        {
            Console.WriteLine("=== ITERATOR PATTERN DEMONSTRATION ===\n");
            
            // 1. Iterate through Bomb collection (List-based)
            Console.WriteLine("--- 1. Bomb Collection (List-based) ---");
            var bombCollection = new BombCollection();
            bombCollection.Add(new Bomb(100, 100, 3));
            bombCollection.Add(new Bomb(200, 200, 5));
            bombCollection.Add(new Bomb(300, 300, 2));
            
            var bombIterator = bombCollection.CreateIterator();
            int bombCount = 0;
            while (bombIterator.HasNext())
            {
                var bomb = bombIterator.Next();
                bombCount++;
                Console.WriteLine($"Bomb {bombCount}: Position ({bomb.X}, {bomb.Y}), Range: {bomb.Range}");
            }
            Console.WriteLine($"Total bombs iterated: {bombCount}\n");
            
            // 2. Iterate through Enemy collection (Dictionary-based)
            Console.WriteLine("--- 2. Enemy Collection (Dictionary-based) ---");
            var enemyCollection = new EnemyCollection();
            var dummyPlayer = new Player(0, 0);
            var enemy1Id = enemyCollection.Add(new Enemy(50, 50, new BasicAIStrategy()));
            var enemy2Id = enemyCollection.Add(new Enemy(150, 150, new AdvancedAIStrategy(dummyPlayer)));
            var enemy3Id = enemyCollection.Add(new Enemy(250, 250, new BasicAIStrategy()));
            
            var enemyIterator = enemyCollection.CreateIterator();
            int enemyCount = 0;
            while (enemyIterator.HasNext())
            {
                var enemy = enemyIterator.Next();
                enemyCount++;
                Console.WriteLine($"Enemy {enemyCount}: Position ({enemy.X}, {enemy.Y}), Speed: {enemy.Speed}");
            }
            Console.WriteLine($"Total enemies iterated: {enemyCount}\n");
            
            // 3. Iterate through Tile collection (2D Array-based)
            Console.WriteLine("--- 3. Tile Grid (2D Array-based) ---");
            var tileGrid = new TileGrid(5, 5);
            tileGrid.SetTile(0, 0, new Tile(0, 0, TileType.Wall));
            tileGrid.SetTile(1, 1, new Tile(1, 1, TileType.BreakableWall));
            tileGrid.SetTile(2, 2, new Tile(2, 2, TileType.Empty));
            tileGrid.SetTile(3, 3, new Tile(3, 3, TileType.BreakableWall));
            tileGrid.SetTile(4, 4, new Tile(4, 4, TileType.Wall));
            
            var tileIterator = tileGrid.CreateIterator();
            int tileCount = 0;
            while (tileIterator.HasNext())
            {
                var tile = tileIterator.Next();
                tileCount++;
                Console.WriteLine($"Tile {tileCount}: Position ({tile.X}, {tile.Y}), Type: {tile.Type}");
            }
            Console.WriteLine($"Total tiles iterated: {tileCount}\n");
            
            // Demonstrate Reset functionality
            Console.WriteLine("--- Demonstrating Reset ---");
            bombIterator.Reset();
            Console.WriteLine("Bomb iterator reset. Iterating again:");
            while (bombIterator.HasNext())
            {
                var bomb = bombIterator.Next();
                Console.WriteLine($"Bomb at ({bomb.X}, {bomb.Y})");
            }
        }
    }
}

