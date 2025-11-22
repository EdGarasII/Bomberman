using System;

namespace BombermanGame.Interpreters
{
    // INTERPRETER PATTERN - Terminal expression: Move command
    public class MoveCommandExpression : IExpression
    {
        private string direction;
        
        public MoveCommandExpression(string direction)
        {
            this.direction = direction.ToUpper();
        }
        
        public void Interpret(GameContext context)
        {
            Console.WriteLine($"[Interpreter] Executing MOVE {direction}");
            context.SetVariable("lastCommand", $"MOVE {direction}");
        }
    }
    
    // INTERPRETER PATTERN - Terminal expression: Bomb command
    public class BombCommandExpression : IExpression
    {
        public void Interpret(GameContext context)
        {
            Console.WriteLine("[Interpreter] Executing PLACE_BOMB");
            context.SetVariable("lastCommand", "PLACE_BOMB");
        }
    }
    
    // INTERPRETER PATTERN - Terminal expression: Pause command
    public class PauseCommandExpression : IExpression
    {
        public void Interpret(GameContext context)
        {
            Console.WriteLine("[Interpreter] Executing PAUSE");
            context.SetVariable("lastCommand", "PAUSE");
        }
    }
    
    // INTERPRETER PATTERN - Terminal expression: Menu command
    public class MenuCommandExpression : IExpression
    {
        public void Interpret(GameContext context)
        {
            Console.WriteLine("[Interpreter] Executing MENU");
            context.SetVariable("lastCommand", "MENU");
        }
    }
    
    // INTERPRETER PATTERN - Non-terminal expression: Command sequence
    public class CommandSequenceExpression : IExpression
    {
        private IExpression[] expressions;
        
        public CommandSequenceExpression(params IExpression[] expressions)
        {
            this.expressions = expressions;
        }
        
        public void Interpret(GameContext context)
        {
            foreach (var expr in expressions)
            {
                expr.Interpret(context);
            }
        }
    }
    
    // INTERPRETER PATTERN - Command parser
    public class CommandParser
    {
        public static IExpression Parse(string command)
        {
            command = command.Trim().ToUpper();
            
            if (command.StartsWith("MOVE"))
            {
                var parts = command.Split(' ');
                if (parts.Length > 1)
                {
                    return new MoveCommandExpression(parts[1]);
                }
            }
            else if (command == "BOMB" || command == "PLACE_BOMB")
            {
                return new BombCommandExpression();
            }
            else if (command == "PAUSE")
            {
                return new PauseCommandExpression();
            }
            else if (command == "MENU" || command == "RETURN_TO_MENU")
            {
                return new MenuCommandExpression();
            }
            else if (command.Contains(";"))
            {
                var commands = command.Split(';');
                var expressions = new IExpression[commands.Length];
                for (int i = 0; i < commands.Length; i++)
                {
                    expressions[i] = Parse(commands[i].Trim());
                }
                return new CommandSequenceExpression(expressions);
            }
            
            throw new ArgumentException($"Unknown command: {command}");
        }
    }
}

