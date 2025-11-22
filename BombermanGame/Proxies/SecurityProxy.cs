using System;

namespace BombermanGame.Proxies
{
    // PROXY PATTERN - Security Proxy (access control)
    public class SecurityProxy : IGameResource
    {
        private GameResource realResource;
        private string userRole;
        
        public SecurityProxy(string resourceName, string role)
        {
            realResource = new GameResource(resourceName);
            userRole = role;
        }
        
        public void Load()
        {
            if (HasAccess())
            {
                realResource.Load();
            }
            else
            {
                throw new UnauthorizedAccessException($"User with role '{userRole}' cannot load resource '{realResource.GetResourceName()}'");
            }
        }
        
        public void Unload()
        {
            if (HasAccess())
            {
                realResource.Unload();
            }
            else
            {
                throw new UnauthorizedAccessException($"User with role '{userRole}' cannot unload resource '{realResource.GetResourceName()}'");
            }
        }
        
        public string GetResourceName()
        {
            return realResource.GetResourceName();
        }
        
        public bool IsLoaded()
        {
            return realResource.IsLoaded();
        }
        
        private bool HasAccess()
        {
            // Only admin can access resources
            return userRole == "Admin";
        }
    }
}

