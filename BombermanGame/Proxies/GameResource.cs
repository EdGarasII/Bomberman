using System;
using System.Diagnostics;
using System.Threading;

namespace BombermanGame.Proxies
{
    // PROXY PATTERN - Real subject (expensive to create)
    public class GameResource : IGameResource
    {
        private string resourceName;
        private bool isLoaded;
        
        public GameResource(string name)
        {
            resourceName = name;
            isLoaded = false;
            // Simulate expensive initialization
            Console.WriteLine($"Creating expensive resource: {name}");
            Thread.Sleep(100); // Simulate loading time
        }
        
        public void Load()
        {
            if (!isLoaded)
            {
                Console.WriteLine($"Loading resource: {resourceName}");
                Thread.Sleep(200); // Simulate loading time
                isLoaded = true;
                Console.WriteLine($"Resource {resourceName} loaded");
            }
        }
        
        public void Unload()
        {
            if (isLoaded)
            {
                Console.WriteLine($"Unloading resource: {resourceName}");
                isLoaded = false;
            }
        }
        
        public string GetResourceName()
        {
            return resourceName;
        }
        
        public bool IsLoaded()
        {
            return isLoaded;
        }
    }
}

