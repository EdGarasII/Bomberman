namespace BombermanGame.Mediators
{
    // MEDIATOR PATTERN - Mediator interface
    public interface IMediator
    {
        void Notify(object sender, string eventType, object data);
    }
}

