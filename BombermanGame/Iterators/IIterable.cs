namespace BombermanGame.Iterators
{
    // ITERATOR PATTERN - Iterable interface
    public interface IIterable<T>
    {
        IIterator<T> CreateIterator();
    }
}

