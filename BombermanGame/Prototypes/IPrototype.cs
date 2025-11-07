namespace BombermanGame.Prototypes
{
    public interface IPrototype<T>
    {
        T Clone();
        
        T DeepClone();
    }
}

