using System;
using System.Windows.Forms;
using BombermanGame.Adapters;

namespace BombermanGame.Managers
{
    public class InputManager
    {
        private IInputAdapter inputAdapter;
        private bool[] keyState;
        
        public InputManager()
        {
            keyState = new bool[256];
            inputAdapter = new KeyboardInputAdapter(keyState);
        }
        
        public void SetInputAdapter(IInputAdapter adapter)
        {
            inputAdapter = adapter;
        }
        
        public void UpdateKeyState(int keyCode, bool isPressed)
        {
            if (keyCode >= 0 && keyCode < keyState.Length)
            {
                keyState[keyCode] = isPressed;
            }
        }
        
        public bool IsMovingUp() => inputAdapter?.IsMovingUp() ?? false;
        public bool IsMovingDown() => inputAdapter?.IsMovingDown() ?? false;
        public bool IsMovingLeft() => inputAdapter?.IsMovingLeft() ?? false;
        public bool IsMovingRight() => inputAdapter?.IsMovingRight() ?? false;
        public bool IsPlacingBomb() => inputAdapter?.IsPlacingBomb() ?? false;
        public bool IsPausing() => inputAdapter?.IsPausing() ?? false;
    }
}

