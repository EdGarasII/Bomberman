using System;
using BombermanGame.Entities;
using BombermanGame.Patterns;

namespace BombermanGame.Templates
{
    // TEMPLATE METHOD PATTERN - Demonstration class
    public class TemplateMethodDemo
    {
        public static void DemonstrateTemplateMethod()
        {
            Console.WriteLine("=== TEMPLATE METHOD PATTERN DEMONSTRATION ===\n");
            
            // Create update templates
            var bombTemplate = new BombUpdateTemplate();
            var enemyTemplate = new EnemyUpdateTemplate();
            var playerTemplate = new PlayerUpdateTemplate();
            
            // Create entities
            var bomb = new Bomb(100, 100, 3);
            var enemy = new Enemy(200, 200, new BasicAIStrategy());
            var player = new Player(300, 300);
            
            Console.WriteLine("--- Updating Bomb using BombUpdateTemplate ---");
            Console.WriteLine($"Bomb timer before: {bomb.Timer}");
            bombTemplate.UpdateEntity(bomb);
            Console.WriteLine($"Bomb timer after: {bomb.Timer}");
            Console.WriteLine($"Bomb active: {bomb.IsActive}\n");
            
            Console.WriteLine("--- Updating Enemy using EnemyUpdateTemplate ---");
            Console.WriteLine($"Enemy position before: ({enemy.X}, {enemy.Y})");
            enemyTemplate.UpdateEntity(enemy);
            Console.WriteLine($"Enemy position after: ({enemy.X}, {enemy.Y})");
            Console.WriteLine($"Enemy active: {enemy.IsActive}\n");
            
            Console.WriteLine("--- Updating Player using PlayerUpdateTemplate ---");
            Console.WriteLine($"Player bomb count before: {player.BombCount}");
            playerTemplate.UpdateEntity(player);
            Console.WriteLine($"Player bomb count after: {player.BombCount}");
            Console.WriteLine($"Player active: {player.IsActive}\n");
            
            // Demonstrate with inactive entity
            Console.WriteLine("--- Testing with inactive entity ---");
            var inactiveBomb = new Bomb(0, 0, 1);
            inactiveBomb.IsActive = false;
            Console.WriteLine($"Inactive bomb timer before: {inactiveBomb.Timer}");
            bombTemplate.UpdateEntity(inactiveBomb);
            Console.WriteLine($"Inactive bomb timer after: {inactiveBomb.Timer} (should be unchanged)\n");
        }
    }
}

