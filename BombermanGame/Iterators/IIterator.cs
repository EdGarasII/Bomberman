namespace BombermanGame.Iterators
{
    // ITERATOR PATTERN - Iterator interface
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
        void Reset();
    }
}

