namespace BombermanGame.Proxies
{
    // PROXY PATTERN - Subject interface
    public interface IGameResource
    {
        void Load();
        void Unload();
        string GetResourceName();
        bool IsLoaded();
    }
}

