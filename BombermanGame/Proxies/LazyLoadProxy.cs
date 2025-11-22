using System;
using System.Diagnostics;

namespace BombermanGame.Proxies
{
    // PROXY PATTERN - Lazy Load Proxy (delayed creation)
    public class LazyLoadProxy : IGameResource
    {
        private GameResource realResource;
        private string resourceName;
        
        public LazyLoadProxy(string name)
        {
            resourceName = name;
            realResource = null; // Not created yet
        }
        
        private GameResource GetRealResource()
        {
            if (realResource == null)
            {
                realResource = new GameResource(resourceName);
            }
            return realResource;
        }
        
        public void Load()
        {
            GetRealResource().Load();
        }
        
        public void Unload()
        {
            if (realResource != null)
            {
                realResource.Unload();
            }
        }
        
        public string GetResourceName()
        {
            return resourceName;
        }
        
        public bool IsLoaded()
        {
            return realResource != null && realResource.IsLoaded();
        }
    }
}

