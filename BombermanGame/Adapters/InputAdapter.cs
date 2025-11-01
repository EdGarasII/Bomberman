using System;
using System.Windows.Forms;
using BombermanGame.Entities;
using BombermanGame.Commands;

namespace BombermanGame.Adapters
{
    // ADAPTER PATTERN - Adapts different input methods to a common interface
    public interface IInputAdapter
    {
        bool IsMovingUp();
        bool IsMovingDown();
        bool IsMovingLeft();
        bool IsMovingRight();
        bool IsPlacingBomb();
        bool IsPausing();
    }
    
    // Keyboard input adapter
    public class KeyboardInputAdapter : IInputAdapter
    {
        private bool[] keys;
        
        public KeyboardInputAdapter(bool[] keyState)
        {
            keys = keyState;
        }
        
        public bool IsMovingUp()
        {
            return keys[(int)Keys.W] || keys[(int)Keys.Up];
        }
        
        public bool IsMovingDown()
        {
            return keys[(int)Keys.S] || keys[(int)Keys.Down];
        }
        
        public bool IsMovingLeft()
        {
            return keys[(int)Keys.A] || keys[(int)Keys.Left];
        }
        
        public bool IsMovingRight()
        {
            return keys[(int)Keys.D] || keys[(int)Keys.Right];
        }
        
        public bool IsPlacingBomb()
        {
            return keys[(int)Keys.Space];
        }
        
        public bool IsPausing()
        {
            return keys[(int)Keys.Escape];
        }
    }
    
    // Gamepad input adapter (placeholder for future implementation)
    public class GamepadInputAdapter : IInputAdapter
    {
        public bool IsMovingUp()
        {
            // TODO: Implement gamepad input
            return false;
        }
        
        public bool IsMovingDown()
        {
            return false;
        }
        
        public bool IsMovingLeft()
        {
            return false;
        }
        
        public bool IsMovingRight()
        {
            return false;
        }
        
        public bool IsPlacingBomb()
        {
            return false;
        }
        
        public bool IsPausing()
        {
            return false;
        }
    }
}

