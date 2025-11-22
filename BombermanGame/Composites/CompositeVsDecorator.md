# Composite Pattern vs Decorator Pattern

## Composite Pattern

**Purpose**: Compose objects into tree structures to represent part-whole hierarchies. Composite lets clients treat individual objects and compositions of objects uniformly.

**Key Characteristics**:
- Represents a tree structure (parent-child relationships)
- Both leaf and composite implement the same interface
- Clients can treat individual objects and compositions uniformly
- Used for hierarchical structures (e.g., file systems, UI components, game object hierarchies)

**Example in Bomberman**:
- `GameComposite` - container for game components
- `GameEntityComponent` - individual game entity
- Can build hierarchies: Level → Room → Entities
- Visibility modes: Public, Protected, Private
- Safety mode: Locked/Unlocked for preventing modifications

**Structure**:
```
IGameComponent (interface)
    ├── GameEntityComponent (leaf)
    └── GameComposite (composite)
        └── SafeGameComposite (composite with safety)
```

**Use Case**: Building hierarchical structures where you need to treat individual objects and groups of objects the same way.

## Decorator Pattern

**Purpose**: Attach additional responsibilities to an object dynamically. Decorators provide a flexible alternative to subclassing for extending functionality.

**Key Characteristics**:
- Wraps an object to add new behaviors
- Maintains the same interface as the wrapped object
- Can stack multiple decorators
- Used for adding features to objects at runtime

**Example in Bomberman**:
- `PowerUpDecorator` - base decorator
- `SpeedPowerUpDecorator`, `BombCountPowerUpDecorator`, etc.
- Wraps a Player object to add power-up effects
- Can stack multiple decorators (Speed + BombCount + Range)

**Structure**:
```
IPlayer (interface)
    ├── Player (concrete)
    └── PowerUpDecorator (decorator)
        ├── SpeedPowerUpDecorator
        ├── BombCountPowerUpDecorator
        └── BombRangePowerUpDecorator
```

**Use Case**: Adding features to objects dynamically without modifying their structure.

## Key Differences

| Aspect | Composite Pattern | Decorator Pattern |
|--------|------------------|-------------------|
| **Purpose** | Represent part-whole hierarchies | Add responsibilities dynamically |
| **Structure** | Tree structure (parent-child) | Wrapper chain (decorator-wrapped) |
| **Relationship** | "Has-a" (contains children) | "Wraps-a" (decorates object) |
| **Focus** | Hierarchical organization | Feature extension |
| **Use Case** | File systems, UI trees, game object hierarchies | Adding features, power-ups, enhancements |
| **Example** | Level contains Rooms, Rooms contain Entities | Player decorated with Speed + BombCount |
| **Modification** | Can add/remove children | Can add/remove decorators |

## When to Use Which?

**Use Composite Pattern when**:
- You need to represent a tree structure
- You want to treat individual objects and groups uniformly
- You need hierarchical organization
- Example: Game level → Rooms → Entities

**Use Decorator Pattern when**:
- You need to add features to objects dynamically
- You want to avoid subclass explosion
- You need to stack multiple features
- Example: Player with multiple power-ups

## Implementation Differences

**Composite**:
```csharp
var level = new GameComposite("Level1");
var room = new GameComposite("Room1");
room.Add(new GameEntityComponent("Enemy1", 100, 100));
level.Add(room);
level.Render(graphics); // Renders all children
```

**Decorator**:
```csharp
IPlayer player = new Player(100, 100);
player = new SpeedPowerUpDecorator(player);
player = new BombCountPowerUpDecorator(player);
player.GetSpeed(); // Returns enhanced speed
```

