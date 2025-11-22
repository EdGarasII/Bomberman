# State Pattern vs Strategy Pattern

## State Pattern

**Purpose**: Allow an object to alter its behavior when its internal state changes. The object will appear to change its class.

**Key Characteristics**:
- States are aware of each other and can transition between states
- State transitions are managed by the context or states themselves
- Each state encapsulates behavior for that specific state
- States can change at runtime based on conditions

**Example in Bomberman**:
- `MenuState`, `PlayingState`, `PausedState`, `GameOverState`, `VictoryState`
- States transition based on game events (player dies → GameOver, level complete → Victory)
- Each state has different behavior (Menu shows options, Playing updates game, Paused freezes game)

**Structure**:
```
GameStateContext
    ├── IGameState (interface)
    │   ├── MenuState
    │   ├── PlayingState
    │   ├── PausedState
    │   ├── GameOverState
    │   └── VictoryState
```

## Strategy Pattern

**Purpose**: Define a family of algorithms, encapsulate each one, and make them interchangeable. Strategy lets the algorithm vary independently from clients that use it.

**Key Characteristics**:
- Strategies are independent and don't know about each other
- Client chooses which strategy to use
- Strategies can be swapped at runtime, but transitions are client-controlled
- Focus is on algorithm selection, not state management

**Example in Bomberman**:
- `IMovementStrategy` with `NormalMovementStrategy`, `FastMovementStrategy`, `SlowMovementStrategy`
- `IAIStrategy` with `BasicAIStrategy`, `AdvancedAIStrategy`
- Player/Enemy chooses a strategy and uses it, but doesn't transition between strategies automatically

**Structure**:
```
Enemy/Player (Context)
    ├── IMovementStrategy (interface)
    │   ├── NormalMovementStrategy
    │   ├── FastMovementStrategy
    │   └── SlowMovementStrategy
```

## Key Differences

| Aspect | State Pattern | Strategy Pattern |
|--------|---------------|-----------------|
| **Purpose** | Manage state transitions and state-specific behavior | Encapsulate interchangeable algorithms |
| **Awareness** | States know about each other and can transition | Strategies are independent and don't know each other |
| **Transitions** | Automatic or state-controlled transitions | Client-controlled selection |
| **Focus** | State management and lifecycle | Algorithm selection |
| **Use Case** | Object behavior changes based on internal state | Different ways to perform the same operation |
| **Example** | Game states (Menu → Playing → Paused) | Movement algorithms (Normal vs Fast vs Slow) |

## When to Use Which?

**Use State Pattern when**:
- Object behavior depends on its state
- State transitions are part of the object's lifecycle
- States need to manage transitions to other states
- Object appears to change its class based on state

**Use Strategy Pattern when**:
- You have multiple ways to perform the same task
- Algorithms should be interchangeable
- You want to avoid conditional statements for algorithm selection
- Algorithms are independent and don't need to know about each other

