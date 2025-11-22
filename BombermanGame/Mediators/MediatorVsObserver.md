# Mediator Pattern vs Observer Pattern

## Mediator Pattern

**Purpose**: Define an object that encapsulates how a set of objects interact. Mediator promotes loose coupling by keeping objects from referring to each other explicitly, and it lets you vary their interaction independently.

**Key Characteristics**:
- Centralized communication hub
- Participants don't know about each other directly
- All communication goes through the mediator
- Reduces dependencies between objects
- Used when many objects need to communicate with each other

**Example in Bomberman**:
- `GameMediator` - central communication hub
- `PlayerManagerParticipant`, `BombManagerParticipant`, `LevelManagerParticipant`
- Participants communicate through mediator, not directly
- When bomb explodes, mediator notifies all participants

**Structure**:
```
IMediator
    └── GameMediator
        ├── PlayerManagerParticipant
        ├── BombManagerParticipant
        └── LevelManagerParticipant
```

**Use Case**: When you have many objects that need to communicate, and you want to avoid tight coupling.

## Observer Pattern

**Purpose**: Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically.

**Key Characteristics**:
- One subject, many observers
- Subject notifies observers directly
- Observers subscribe/unsubscribe to subject
- Used for event-driven architectures
- Loose coupling between subject and observers

**Example in Bomberman**:
- `GameEventSystem` - subject
- Event handlers subscribe to events
- When event occurs, all subscribers are notified
- Events: BombExploded, PlayerDied, PowerUpCollected

**Structure**:
```
GameEventSystem (Subject)
    ├── IEventHandler (Observer interface)
    │   ├── BombExplodedHandler
    │   ├── PlayerDiedHandler
    │   └── PowerUpCollectedHandler
```

**Use Case**: When you need to notify multiple objects about state changes in one object.

## Key Differences

| Aspect | Mediator Pattern | Observer Pattern |
|--------|------------------|------------------|
| **Purpose** | Centralize communication between many objects | Notify observers about subject changes |
| **Structure** | Many-to-many through mediator | One-to-many (subject to observers) |
| **Communication** | All communication through mediator | Direct notification from subject |
| **Coupling** | Participants don't know each other | Observers know about subject |
| **Focus** | Coordinating interactions | Broadcasting state changes |
| **Use Case** | Complex interactions between many objects | Event notifications, state changes |

## When to Use Which?

**Use Mediator Pattern when**:
- Many objects need to communicate with each other
- You want to avoid tight coupling between objects
- Communication logic is complex
- You want centralized control over interactions
- Example: Game managers coordinating through mediator

**Use Observer Pattern when**:
- One object needs to notify many others
- You need event-driven architecture
- You want loose coupling between subject and observers
- You need publish-subscribe mechanism
- Example: Game events notifying handlers

## Implementation Differences

**Mediator**:
```csharp
var mediator = new GameMediator();
var playerMgr = new PlayerManagerParticipant("PlayerMgr");
var bombMgr = new BombManagerParticipant("BombMgr");
mediator.Register(playerMgr);
mediator.Register(bombMgr);

playerMgr.PlaceBomb(); // Notifies through mediator
// Mediator notifies bombMgr
```

**Observer**:
```csharp
var eventSystem = GameEventSystem.Instance;
eventSystem.Subscribe("BombExploded", handler);
eventSystem.Notify("BombExploded", data);
// All subscribers notified directly
```

