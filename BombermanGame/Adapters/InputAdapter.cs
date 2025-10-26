using System;
using System.Windows.Forms;
using BombermanGame.Entities;
using BombermanGame.Commands;

namespace BombermanGame.Adapters
{
    // ADAPTER PATTERN - Adapts different input methods to a common interface
    
    // Target interface - 6 methods
    public interface IInputAdapter
    {
        bool IsMovingUp();
        bool IsMovingDown();
        bool IsMovingLeft();
        bool IsMovingRight();
        bool IsPlacingBomb();
        bool IsPausing();
    }
    
    // ADAPTEE CLASS - Raw keyboard input with 12 methods (different count from adapter)
    public class RawKeyboardInput
    {
        private bool[] keyState;
        
        public RawKeyboardInput(bool[] keys)
        {
            keyState = keys;
        }
        
        // Individual key check methods (12 methods total)
        public bool IsKeyWPressed() => keyState[(int)Keys.W];
        public bool IsKeyAPressed() => keyState[(int)Keys.A];
        public bool IsKeySPressed() => keyState[(int)Keys.S];
        public bool IsKeyDPressed() => keyState[(int)Keys.D];
        public bool IsUpArrowPressed() => keyState[(int)Keys.Up];
        public bool IsDownArrowPressed() => keyState[(int)Keys.Down];
        public bool IsLeftArrowPressed() => keyState[(int)Keys.Left];
        public bool IsRightArrowPressed() => keyState[(int)Keys.Right];
        public bool IsSpacePressed() => keyState[(int)Keys.Space];
        public bool IsEscapePressed() => keyState[(int)Keys.Escape];
        public bool IsEnterPressed() => keyState[(int)Keys.Enter];
        public bool IsShiftPressed() => keyState[(int)Keys.Shift];
        
        public bool[] GetAllKeyStates() => keyState;
    }
    
    // ADAPTER - Adapts RawKeyboardInput (12 methods) to IInputAdapter (6 methods)
    public class KeyboardInputAdapter : IInputAdapter
    {
        private RawKeyboardInput rawInput;
        
        public KeyboardInputAdapter(RawKeyboardInput rawKeyboardInput)
        {
            rawInput = rawKeyboardInput;
        }
        
        // Convenience constructor that accepts bool array
        public KeyboardInputAdapter(bool[] keyState)
        {
            rawInput = new RawKeyboardInput(keyState);
        }
        
        public bool IsMovingUp()
        {
            return rawInput.IsKeyWPressed() || rawInput.IsUpArrowPressed();
        }
        
        public bool IsMovingDown()
        {
            return rawInput.IsKeySPressed() || rawInput.IsDownArrowPressed();
        }
        
        public bool IsMovingLeft()
        {
            return rawInput.IsKeyAPressed() || rawInput.IsLeftArrowPressed();
        }
        
        public bool IsMovingRight()
        {
            return rawInput.IsKeyDPressed() || rawInput.IsRightArrowPressed();
        }
        
        public bool IsPlacingBomb()
        {
            return rawInput.IsSpacePressed();
        }
        
        public bool IsPausing()
        {
            return rawInput.IsEscapePressed();
        }
    }
    
    // ADAPTEE CLASS 2 - Raw gamepad input with 10 methods (different count)
    public class RawGamepadInput
    {
        private float leftStickX;
        private float leftStickY;
        private bool buttonA;
        private bool buttonB;
        private bool buttonStart;
        
        public RawGamepadInput()
        {
            leftStickX = 0f;
            leftStickY = 0f;
            buttonA = false;
            buttonB = false;
            buttonStart = false;
        }
        
        // 10 individual methods for gamepad state
        public float GetLeftStickX() => leftStickX;
        public float GetLeftStickY() => leftStickY;
        public void SetLeftStickX(float value) => leftStickX = value;
        public void SetLeftStickY(float value) => leftStickY = value;
        public bool IsButtonAPressed() => buttonA;
        public bool IsButtonBPressed() => buttonB;
        public bool IsButtonStartPressed() => buttonStart;
        public void SetButtonA(bool pressed) => buttonA = pressed;
        public void SetButtonB(bool pressed) => buttonB = pressed;
        public void SetButtonStart(bool pressed) => buttonStart = pressed;
    }
    
    // ADAPTER 2 - Adapts RawGamepadInput (10 methods) to IInputAdapter (6 methods)
    public class GamepadInputAdapter : IInputAdapter
    {
        private RawGamepadInput gamepadInput;
        private const float STICK_THRESHOLD = 0.5f;
        
        public GamepadInputAdapter(RawGamepadInput gamepad)
        {
            gamepadInput = gamepad;
        }
        
        public GamepadInputAdapter()
        {
            gamepadInput = new RawGamepadInput();
        }
        
        public bool IsMovingUp()
        {
            return gamepadInput.GetLeftStickY() < -STICK_THRESHOLD;
        }
        
        public bool IsMovingDown()
        {
            return gamepadInput.GetLeftStickY() > STICK_THRESHOLD;
        }
        
        public bool IsMovingLeft()
        {
            return gamepadInput.GetLeftStickX() < -STICK_THRESHOLD;
        }
        
        public bool IsMovingRight()
        {
            return gamepadInput.GetLeftStickX() > STICK_THRESHOLD;
        }
        
        public bool IsPlacingBomb()
        {
            return gamepadInput.IsButtonAPressed();
        }
        
        public bool IsPausing()
        {
            return gamepadInput.IsButtonStartPressed();
        }
    }
}

