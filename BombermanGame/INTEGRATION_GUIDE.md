# Integracijos vadovas - Naujų šablonų integravimas į žaidimą

## Reikalingi pakeitimai GameForm.cs

### 1. State Pattern - Pakeisti GameManager enum į State Pattern
**Dabar**: `GameManager.CurrentState` naudoja enum
**Reikia**: Naudoti `GameStateContext` su State Pattern

### 2. Iterator Pattern - Pakeisti foreach ciklus
**Dabar**: `foreach (var bomb in bombs)`
**Reikia**: Naudoti `BombCollection` su iterator

### 3. Template Method - Entity updates
**Dabar**: `bomb.Update()`, `enemy.Update()`, `player.Update()`
**Reikia**: Naudoti `BombUpdateTemplate`, `EnemyUpdateTemplate`, `PlayerUpdateTemplate`

### 4. Visitor Pattern - Rendering ir updates
**Dabar**: `RenderingManager.Render()` tiesiogiai
**Reikia**: Naudoti `RenderVisitor` ir `UpdateVisitor`

### 5. Flyweight Pattern - Tile optimizacija
**Dabar**: Kiekvienas `Tile` objektas turi visą informaciją
**Reikia**: Naudoti `TileFlyweight` su `TileContext`

### 6. Composite Pattern - Game object hierarchy
**Dabar**: Plokščia struktūra
**Reikia**: Naudoti `GameComposite` hierarchijai

### 7. Chain Of Responsibility - Input/Event handling
**Dabar**: Tiesioginis input handling
**Reikia**: Naudoti handler grandinėlę

### 8. Mediator Pattern - Manager communication
**Dabar**: Manageriai komunikuoja tiesiogiai
**Reikia**: Naudoti `GameMediator`

### 9. Memento Pattern - Save/Load
**Dabar**: Nėra save/load
**Reikia**: Pridėti save/load su Memento

### 10. Proxy Pattern - Resource loading
**Dabar**: Nėra resource management
**Reikia**: Naudoti Proxy resource loading

### 11. Interpreter Pattern - Console commands
**Dabar**: Nėra console
**Reikia**: Pridėti console su command interpreter

