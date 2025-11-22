using System;
using System.Collections.Generic;
using System.Drawing;

namespace BombermanGame.Composites
{
    // COMPOSITE PATTERN - Component interface
    public interface IGameComponent
    {
        void Render(Graphics graphics);
        void Update();
        void Add(IGameComponent component);
        void Remove(IGameComponent component);
        IGameComponent GetChild(int index);
        int GetChildCount();
        string GetName();
    }
    
    // COMPOSITE PATTERN - Leaf (individual component)
    public class GameEntityComponent : IGameComponent
    {
        private string name;
        private int x, y;
        
        public GameEntityComponent(string name, int x, int y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }
        
        public void Render(Graphics graphics)
        {
            using (Brush brush = new SolidBrush(Color.Blue))
            {
                graphics.FillEllipse(brush, x, y, 20, 20);
            }
        }
        
        public void Update()
        {
            // Leaf update logic
        }
        
        public void Add(IGameComponent component)
        {
            throw new NotSupportedException("Cannot add to leaf component");
        }
        
        public void Remove(IGameComponent component)
        {
            throw new NotSupportedException("Cannot remove from leaf component");
        }
        
        public IGameComponent GetChild(int index)
        {
            throw new NotSupportedException("Leaf has no children");
        }
        
        public int GetChildCount()
        {
            return 0;
        }
        
        public string GetName()
        {
            return name;
        }
    }
    
    // COMPOSITE PATTERN - Composite (container component)
    public class GameComposite : IGameComponent
    {
        private string name;
        private List<IGameComponent> children;
        private VisibilityMode visibility;
        
        public enum VisibilityMode
        {
            Public,      // All children visible
            Protected,   // Only active children visible
            Private      // No children visible
        }
        
        public GameComposite(string name, VisibilityMode visibility = VisibilityMode.Public)
        {
            this.name = name;
            this.children = new List<IGameComponent>();
            this.visibility = visibility;
        }
        
        public void Render(Graphics graphics)
        {
            if (visibility == VisibilityMode.Private)
                return;
                
            foreach (var child in children)
            {
                if (visibility == VisibilityMode.Public || 
                    (visibility == VisibilityMode.Protected && child is GameEntityComponent))
                {
                    child.Render(graphics);
                }
            }
        }
        
        public void Update()
        {
            foreach (var child in children)
            {
                child.Update();
            }
        }
        
        public void Add(IGameComponent component)
        {
            children.Add(component);
        }
        
        public void Remove(IGameComponent component)
        {
            children.Remove(component);
        }
        
        public IGameComponent GetChild(int index)
        {
            if (index >= 0 && index < children.Count)
                return children[index];
            return null;
        }
        
        public int GetChildCount()
        {
            return children.Count;
        }
        
        public string GetName()
        {
            return name;
        }
        
        public void SetVisibility(VisibilityMode mode)
        {
            visibility = mode;
        }
        
        public VisibilityMode GetVisibility()
        {
            return visibility;
        }
    }
    
    // COMPOSITE PATTERN - Safety mode implementation
    public class SafeGameComposite : GameComposite
    {
        private bool isLocked;
        
        public SafeGameComposite(string name, VisibilityMode visibility = VisibilityMode.Public) 
            : base(name, visibility)
        {
            isLocked = false;
        }
        
        public void Lock()
        {
            isLocked = true;
        }
        
        public void Unlock()
        {
            isLocked = false;
        }
        
        public new void Add(IGameComponent component)
        {
            if (isLocked)
                throw new InvalidOperationException("Cannot add component: composite is locked");
            base.Add(component);
        }
        
        public new void Remove(IGameComponent component)
        {
            if (isLocked)
                throw new InvalidOperationException("Cannot remove component: composite is locked");
            base.Remove(component);
        }
    }
}

