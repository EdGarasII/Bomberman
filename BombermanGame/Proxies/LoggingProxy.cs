using System;
using System.Diagnostics;

namespace BombermanGame.Proxies
{
    // PROXY PATTERN - Logging Proxy (added functionality)
    public class LoggingProxy : IGameResource
    {
        private GameResource realResource;
        private int accessCount;
        
        public LoggingProxy(string resourceName)
        {
            realResource = new GameResource(resourceName);
            accessCount = 0;
        }
        
        public void Load()
        {
            var stopwatch = Stopwatch.StartNew();
            accessCount++;
            Console.WriteLine($"[LOG] Loading resource '{realResource.GetResourceName()}' (Access #{accessCount})");
            
            realResource.Load();
            
            stopwatch.Stop();
            Console.WriteLine($"[LOG] Resource loaded in {stopwatch.ElapsedMilliseconds} ms");
        }
        
        public void Unload()
        {
            Console.WriteLine($"[LOG] Unloading resource '{realResource.GetResourceName()}'");
            realResource.Unload();
        }
        
        public string GetResourceName()
        {
            return realResource.GetResourceName();
        }
        
        public bool IsLoaded()
        {
            return realResource.IsLoaded();
        }
        
        public int GetAccessCount()
        {
            return accessCount;
        }
    }
}

