using System;

namespace BombermanGame.ChainOfResponsibility
{
    // CHAIN OF RESPONSIBILITY PATTERN - Handler interface
    public abstract class RequestHandler
    {
        protected RequestHandler nextHandler;
        
        public void SetNext(RequestHandler handler)
        {
            nextHandler = handler;
        }
        
        public abstract void HandleRequest(GameRequest request);
        
        protected void PassToNext(GameRequest request)
        {
            if (nextHandler != null)
            {
                nextHandler.HandleRequest(request);
            }
            else
            {
                Console.WriteLine($"Request '{request.Type}' was not handled by any handler");
            }
        }
    }
    
    // CHAIN OF RESPONSIBILITY PATTERN - Request class
    public class GameRequest
    {
        public string Type { get; }
        public string Data { get; }
        public int Priority { get; }
        
        public GameRequest(string type, string data, int priority = 0)
        {
            Type = type;
            Data = data;
            Priority = priority;
        }
    }
    
    // CHAIN OF RESPONSIBILITY PATTERN - Handler 1: Input Handler
    public class InputHandler : RequestHandler
    {
        public override void HandleRequest(GameRequest request)
        {
            if (request.Type == "Input")
            {
                Console.WriteLine($"[InputHandler] Processing input: {request.Data}");
            }
            else
            {
                PassToNext(request);
            }
        }
    }
    
    // CHAIN OF RESPONSIBILITY PATTERN - Handler 2: Movement Handler
    public class MovementHandler : RequestHandler
    {
        public override void HandleRequest(GameRequest request)
        {
            if (request.Type == "Movement")
            {
                Console.WriteLine($"[MovementHandler] Processing movement: {request.Data}");
            }
            else
            {
                PassToNext(request);
            }
        }
    }
    
    // CHAIN OF RESPONSIBILITY PATTERN - Handler 3: Collision Handler
    public class CollisionHandler : RequestHandler
    {
        public override void HandleRequest(GameRequest request)
        {
            if (request.Type == "Collision")
            {
                Console.WriteLine($"[CollisionHandler] Processing collision: {request.Data}");
            }
            else
            {
                PassToNext(request);
            }
        }
    }
    
    // CHAIN OF RESPONSIBILITY PATTERN - Handler 4: Event Handler
    public class EventHandler : RequestHandler
    {
        public override void HandleRequest(GameRequest request)
        {
            if (request.Type == "Event")
            {
                Console.WriteLine($"[EventHandler] Processing event: {request.Data}");
            }
            else
            {
                PassToNext(request);
            }
        }
    }
    
    // CHAIN OF RESPONSIBILITY PATTERN - Chain builder
    public class RequestChainBuilder
    {
        public static RequestHandler BuildChain()
        {
            var inputHandler = new InputHandler();
            var movementHandler = new MovementHandler();
            var collisionHandler = new CollisionHandler();
            var eventHandler = new EventHandler();
            
            inputHandler.SetNext(movementHandler);
            movementHandler.SetNext(collisionHandler);
            collisionHandler.SetNext(eventHandler);
            
            return inputHandler;
        }
    }
}

