using System.Collections.Generic;
using BombermanGame.Entities;

namespace BombermanGame.Iterators
{
    // ITERATOR PATTERN - Bomb collection using List<T>
    public class BombCollection : IIterable<Bomb>
    {
        private List<Bomb> bombs;
        
        public BombCollection()
        {
            bombs = new List<Bomb>();
        }
        
        public void Add(Bomb bomb)
        {
            bombs.Add(bomb);
        }
        
        public void Remove(Bomb bomb)
        {
            bombs.Remove(bomb);
        }
        
        public int Count => bombs.Count;
        
        public Bomb this[int index] => bombs[index];
        
        public IIterator<Bomb> CreateIterator()
        {
            return new BombIterator(this);
        }
    }
    
    // ITERATOR PATTERN - Iterator for Bomb collection (List-based)
    public class BombIterator : IIterator<Bomb>
    {
        private BombCollection collection;
        private int currentIndex;
        
        public BombIterator(BombCollection collection)
        {
            this.collection = collection;
            currentIndex = 0;
        }
        
        public bool HasNext()
        {
            return currentIndex < collection.Count;
        }
        
        public Bomb Next()
        {
            if (!HasNext())
                throw new System.InvalidOperationException("No more elements");
                
            return collection[currentIndex++];
        }
        
        public void Reset()
        {
            currentIndex = 0;
        }
    }
}

