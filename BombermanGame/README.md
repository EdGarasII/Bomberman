# Bomberman Game in C#

A complete Bomberman game implementation in C# using System.Drawing and implementing all major design patterns.

## Design Patterns Implemented

### 1. **Singleton Pattern**
- `GameManager` - Ensures only one game instance exists
- `GameEventSystem` - Centralized event management

### 2. **Factory Pattern**
- `EntityFactory` - Creates game entities (players, bombs, enemies, power-ups)
- `AbstractEntityFactory` - Abstract factory for different entity types
- `StandardEntityFactory` & `EnhancedEntityFactory` - Concrete implementations

### 3. **Strategy Pattern**
- `IMovementStrategy` - Different movement behaviors for entities
- `IAIStrategy` - Different AI behaviors for enemies
- `NormalMovementStrategy`, `FastMovementStrategy`, `SlowMovementStrategy`
- `BasicAIStrategy`, `AdvancedAIStrategy`

### 4. **Observer Pattern**
- `GameEventSystem` - Event notification system
- `IEventHandler` - Event handling interface
- Events: BombExploded, PlayerDied, PowerUpCollected, etc.

### 5. **Builder Pattern**
- `LevelBuilder` - Constructs game levels step by step
- Fluent interface for easy level creation
- Supports different entity factories

### 6. **Prototype Pattern**
- `EntityPrototype` - Clones existing entities
- Registry of prototype entities
- `Clone()` method in all game entities

### 7. **Decorator Pattern**
- `PowerUpDecorator` - Enhances player abilities
- `SpeedPowerUpDecorator`, `BombCountPowerUpDecorator`, etc.
- `PowerUpDecoratorFactory` - Creates decorated players

### 8. **Command Pattern**
- `ICommand` - Encapsulates player actions
- `MoveCommand`, `PlaceBombCommand`
- `CommandInvoker` - Executes and manages commands with undo/redo

### 9. **Adapter Pattern**
- `IInputAdapter` - Adapts different input methods
- `KeyboardInputAdapter`, `GamepadInputAdapter`
- `InputManager` - Manages input handling

### 10. **Facade Pattern**
- `GameFacade` - Simplified interface to complex game systems
- Hides complexity of multiple subsystems
- Single entry point for game operations

### 11. **Bridge Pattern**
- `RenderingSystem` - Separates rendering logic from implementation
- `IRenderer` - Rendering interface
- `BasicRenderer`, `AdvancedRenderer`
- `StandardRenderingSystem`, `EnhancedRenderingSystem`

## Project Structure

```
BombermanGame/
├── Core/
│   ├── GameManager.cs          # Singleton - Main game controller
│   └── GameState.cs            # Game state management
├── Entities/
│   ├── GameEntity.cs           # Base entity class with Prototype pattern
│   ├── Player.cs               # Player entity
│   ├── Bomb.cs                 # Bomb entity
│   ├── Enemy.cs                # Enemy entity
│   ├── PowerUp.cs              # Power-up entity
│   ├── Tile.cs                 # Tile entity
│   └── Explosion.cs            # Explosion effects
├── Patterns/
│   ├── IMovementStrategy.cs    # Strategy pattern for movement
│   └── IAIStrategy.cs          # Strategy pattern for AI
├── Factories/
│   ├── EntityFactory.cs        # Factory pattern
│   └── AbstractEntityFactory.cs # Abstract factory pattern
├── Observers/
│   └── GameEventSystem.cs      # Observer pattern
├── Commands/
│   ├── ICommand.cs             # Command pattern
│   └── CommandInvoker.cs       # Command invoker
├── Builders/
│   └── LevelBuilder.cs         # Builder pattern
├── Prototypes/
│   └── EntityPrototype.cs      # Prototype pattern
├── Decorators/
│   └── PowerUpDecorator.cs     # Decorator pattern
├── Adapters/
│   └── InputAdapter.cs         # Adapter pattern
├── Facades/
│   └── GameFacade.cs           # Facade pattern
├── Bridges/
│   └── RenderingBridge.cs      # Bridge pattern
├── Managers/
│   ├── InputManager.cs
│   ├── RenderingManager.cs
│   ├── LevelManager.cs
│   ├── PlayerManager.cs
│   ├── BombManager.cs
│   └── PowerUpManager.cs
├── bin/                        # Build output
├── obj/                        # Build intermediates
├── GameForm.cs                 # Main game form
├── Program.cs                  # Entry point
├── BombermanGame.csproj        # Project file
└── README.md                   # This file
```

## How to Run

1. **Prerequisites:**
   - .NET 6.0 or later
   - Windows (for System.Drawing)

2. **Build and Run:**
   ```bash
   cd BombermanGame
   dotnet build
   dotnet run
   ```

3. **Controls:**
   - **WASD** or **Arrow Keys** - Move player
   - **Space** - Place bomb
   - **ESC** - Pause game

## Game Features

- **Player Movement** - Smooth movement with collision detection
- **Bomb Mechanics** - Place bombs with timer and explosion
- **Enemy AI** - Different AI strategies for enemies
- **Power-ups** - Various power-ups that enhance player abilities
- **Level System** - Procedurally generated levels
- **Event System** - Observer pattern for game events
- **Command System** - Undo/redo functionality
- **Multiple Rendering** - Basic and enhanced rendering modes

## Design Pattern Benefits

- **Maintainability** - Clear separation of concerns
- **Extensibility** - Easy to add new features
- **Testability** - Each pattern can be tested independently
- **Flexibility** - Easy to swap implementations
- **Code Reuse** - Patterns promote reusable components

## Future Enhancements

- Multiplayer support
- Sound effects and music
- Particle effects
- More enemy types
- Level editor
- Save/load system
- Network play

