using System;
using BombermanGame.States;
using BombermanGame.Iterators;
using BombermanGame.Templates;
using BombermanGame.Flyweights;
using BombermanGame.Proxies;
using BombermanGame.ChainOfResponsibility;
using BombermanGame.Visitors;
using BombermanGame.Interpreters;
using BombermanGame.Mediators;
using BombermanGame.Mementos;
using BombermanGame.Composites;
using System.Drawing;

namespace BombermanGame
{
    // Comprehensive demo for all Part 2 patterns
    public class AllPatternsDemo
    {
        public static void RunAllDemos()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║     BOMBERMAN GAME - ALL PATTERNS DEMONSTRATION         ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");
            
            // 1. State Pattern
            Console.WriteLine("\n[1] STATE PATTERN");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            StatePatternDemo.DemonstrateStatePattern();
            
            // 2. Iterator Pattern
            Console.WriteLine("\n[2] ITERATOR PATTERN");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            IteratorPatternDemo.DemonstrateIteratorPattern();
            
            // 3. Template Method Pattern
            Console.WriteLine("\n[3] TEMPLATE METHOD PATTERN");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            TemplateMethodDemo.DemonstrateTemplateMethod();
            
            // 4. Flyweight Pattern
            Console.WriteLine("\n[4] FLYWEIGHT PATTERN");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            FlyweightPerformanceTest.RunPerformanceTest();
            
            // 5. Composite Pattern
            Console.WriteLine("\n[5] COMPOSITE PATTERN");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            DemonstrateComposite();
            
            // 6. Proxy Pattern
            Console.WriteLine("\n[6] PROXY PATTERN");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            ProxyPerformanceTest.RunPerformanceTest();
            
            // 7. Chain Of Responsibility Pattern
            Console.WriteLine("\n[7] CHAIN OF RESPONSIBILITY PATTERN");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            DemonstrateChainOfResponsibility();
            
            // 8. Visitor Pattern
            Console.WriteLine("\n[8] VISITOR PATTERN");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            VisitorPatternDemo.DemonstrateVisitorPattern();
            
            // 9. Interpreter Pattern
            Console.WriteLine("\n[9] INTERPRETER PATTERN");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            DemonstrateInterpreter();
            
            // 10. Mediator Pattern
            Console.WriteLine("\n[10] MEDIATOR PATTERN");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            DemonstrateMediator();
            
            // 11. Memento Pattern
            Console.WriteLine("\n[11] MEMENTO PATTERN");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            DemonstrateMemento();
            
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           ALL PATTERNS DEMONSTRATION COMPLETE            ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        }
        
        private static void DemonstrateComposite()
        {
            var level = new GameComposite("Level1", GameComposite.VisibilityMode.Public);
            var room = new GameComposite("Room1", GameComposite.VisibilityMode.Public);
            room.Add(new GameEntityComponent("Enemy1", 100, 100));
            room.Add(new GameEntityComponent("Enemy2", 200, 200));
            level.Add(room);
            
            Console.WriteLine($"Level has {level.GetChildCount()} children");
            Console.WriteLine($"Room has {room.GetChildCount()} children");
            
            var safeComposite = new SafeGameComposite("SafeLevel", GameComposite.VisibilityMode.Public);
            safeComposite.Add(new GameEntityComponent("Player", 50, 50));
            safeComposite.Lock();
            Console.WriteLine("Safe composite locked");
        }
        
        private static void DemonstrateChainOfResponsibility()
        {
            var chain = RequestChainBuilder.BuildChain();
            
            chain.HandleRequest(new GameRequest("Input", "W key pressed"));
            chain.HandleRequest(new GameRequest("Movement", "Move up"));
            chain.HandleRequest(new GameRequest("Collision", "Wall collision"));
            chain.HandleRequest(new GameRequest("Event", "Bomb exploded"));
            chain.HandleRequest(new GameRequest("Unknown", "Unknown request"));
        }
        
        private static void DemonstrateInterpreter()
        {
            var context = new GameContext();
            
            var commands = new[]
            {
                "MOVE UP",
                "BOMB",
                "PAUSE",
                "MOVE LEFT; BOMB; MOVE RIGHT"
            };
            
            foreach (var cmd in commands)
            {
                Console.WriteLine($"\nParsing command: {cmd}");
                try
                {
                    var expression = CommandParser.Parse(cmd);
                    expression.Interpret(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        
        private static void DemonstrateMediator()
        {
            var mediator = new GameMediator();
            var playerMgr = new PlayerManagerParticipant("PlayerManager");
            var bombMgr = new BombManagerParticipant("BombManager");
            var levelMgr = new LevelManagerParticipant("LevelManager");
            
            mediator.Register(playerMgr);
            mediator.Register(bombMgr);
            mediator.Register(levelMgr);
            
            Console.WriteLine("Mediator registered 3 participants");
            playerMgr.PlaceBomb();
            bombMgr.ExplodeBomb();
            levelMgr.UpdateLevel();
        }
        
        private static void DemonstrateMemento()
        {
            var originator = new GameOriginator();
            originator.SetState("Score", 1000);
            originator.SetState("Level", 3);
            originator.SetState("Lives", 2);
            
            Console.WriteLine("Initial state:");
            originator.DisplayState();
            
            var caretaker = new GameCaretaker();
            var memento1 = originator.CreateMemento();
            caretaker.SaveMemento(memento1);
            
            originator.SetState("Score", 1500);
            originator.SetState("Level", 4);
            Console.WriteLine("\nState after changes:");
            originator.DisplayState();
            
            var memento2 = originator.CreateMemento();
            caretaker.SaveMemento(memento2);
            
            Console.WriteLine($"\nRestoring from memento 1 (saved {memento1.GetTimestamp()})");
            originator.RestoreFromMemento(caretaker.GetMemento(0));
            originator.DisplayState();
            
            Console.WriteLine($"\nTotal mementos saved: {caretaker.GetMementoCount()}");
        }
    }
}

