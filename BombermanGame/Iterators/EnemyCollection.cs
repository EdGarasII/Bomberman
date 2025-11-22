using System.Collections;
using System.Collections.Generic;
using BombermanGame.Entities;

namespace BombermanGame.Iterators
{
    // ITERATOR PATTERN - Enemy collection using Dictionary<int, Enemy>
    public class EnemyCollection : IIterable<Enemy>
    {
        private Dictionary<int, Enemy> enemies;
        private int nextId;
        
        public EnemyCollection()
        {
            enemies = new Dictionary<int, Enemy>();
            nextId = 0;
        }
        
        public int Add(Enemy enemy)
        {
            int id = nextId++;
            enemies[id] = enemy;
            return id;
        }
        
        public void Remove(int id)
        {
            enemies.Remove(id);
        }
        
        public Enemy Get(int id)
        {
            return enemies[id];
        }
        
        public int Count => enemies.Count;
        
        public IEnumerable<Enemy> GetAllEnemies()
        {
            return enemies.Values;
        }
        
        public IIterator<Enemy> CreateIterator()
        {
            return new EnemyIterator(this);
        }
    }
    
    // ITERATOR PATTERN - Iterator for Enemy collection (Dictionary-based)
    public class EnemyIterator : IIterator<Enemy>
    {
        private EnemyCollection collection;
        private List<Enemy> enemyList;
        private int currentIndex;
        
        public EnemyIterator(EnemyCollection collection)
        {
            this.collection = collection;
            // Convert dictionary values to list for iteration
            enemyList = new List<Enemy>();
            foreach (var enemy in collection.GetAllEnemies())
            {
                enemyList.Add(enemy);
            }
            currentIndex = 0;
        }
        
        public bool HasNext()
        {
            return currentIndex < enemyList.Count;
        }
        
        public Enemy Next()
        {
            if (!HasNext())
                throw new System.InvalidOperationException("No more elements");
                
            return enemyList[currentIndex++];
        }
        
        public void Reset()
        {
            currentIndex = 0;
            enemyList = new List<Enemy>();
            foreach (var enemy in collection.GetAllEnemies())
            {
                enemyList.Add(enemy);
            }
        }
    }
}

