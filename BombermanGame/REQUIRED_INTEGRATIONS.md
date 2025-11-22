# Reikalingi pakeitimai žaidime

## ✅ PRIVALOMI INTEGRACIJOS PUNKTAI (gynimo metu gali būti paklausti)

### 1. State Pattern - GameManager
**Dabar**: `GameManager.CurrentState` naudoja enum
**Reikia pakeisti į**: `GameStateContext` su State Pattern

**Pakeitimai**:
- `Core/GameManager.cs` - pridėti `GameStateContext`
- `GameForm.cs` - naudoti state context vietoj enum

### 2. Iterator Pattern - Collections
**Dabar**: `foreach (var bomb in bombs)`
**Reikia pakeisti į**: Naudoti `BombCollection`, `EnemyCollection` su iteratoriais

**Pakeitimai**:
- `GameForm.cs` - pakeisti `List<Bomb>` į `BombCollection`
- `GameForm.cs` - pakeisti `List<Enemy>` į `EnemyCollection`
- Naudoti iteratorius vietoj foreach

### 3. Template Method - Entity Updates
**Dabar**: `bomb.Update()`, `enemy.Update()`, `player.Update()`
**Reikia pakeisti į**: Naudoti template metodus

**Pakeitimai**:
- `GameForm.cs` - `UpdateBombs()` naudoja `BombUpdateTemplate`
- `GameForm.cs` - `UpdateEnemies()` naudoja `EnemyUpdateTemplate`
- `GameForm.cs` - player update naudoja `PlayerUpdateTemplate`

### 4. Visitor Pattern - Rendering
**Dabar**: `RenderingManager.Render()` tiesiogiai
**Reikia pakeisti į**: Naudoti `RenderVisitor`

**Pakeitimai**:
- `GameForm.cs` - `OnPaint()` naudoja `RenderVisitor`
- Arba `RenderingManager` naudoja visitor pattern

### 5. Flyweight Pattern - Tiles
**Dabar**: Kiekvienas `Tile` objektas turi visą informaciją
**Reikia pakeisti į**: Naudoti `TileFlyweight` su `TileContext`

**Pakeitimai**:
- `GameForm.cs` - `board` naudoja `TileContext` vietoj `Tile[,]`
- Arba `Tile` klasė naudoja flyweight factory

## ⚠️ REKOMENDUOJAMI (bet ne privalomi)

### 6. Composite Pattern - Game Hierarchy
- Galima naudoti `GameComposite` hierarchijai (Level → Room → Entities)
- Dabar nėra būtina, bet gali pagerinti struktūrą

### 7. Chain Of Responsibility - Input Handling
- Galima naudoti handler grandinėlę input apdorojimui
- Dabar `HandlePlayerMovement()` tiesioginis

### 8. Mediator Pattern - Manager Communication
- Galima naudoti `GameMediator` managerių komunikacijai
- Dabar manageriai komunikuoja tiesiogiai

### 9. Memento Pattern - Save/Load
- Pridėti save/load funkcionalumą
- Dabar nėra save/load

### 10. Proxy Pattern - Resource Loading
- Galima naudoti resource loading su proxy
- Dabar nėra resource management

### 11. Interpreter Pattern - Console Commands
- Pridėti console su command interpreter
- Dabar nėra console

## 📝 PRIORITETAI

**AUKŠTAS PRIORITETAS** (gynimo metu tikrai paklaus):
1. ✅ State Pattern - GameManager
2. ✅ Iterator Pattern - Collections
3. ✅ Template Method - Updates
4. ✅ Visitor Pattern - Rendering
5. ✅ Flyweight Pattern - Tiles

**VIDUTINIS PRIORITETAS**:
6. Composite Pattern
7. Chain Of Responsibility
8. Mediator Pattern

**ŽEMAS PRIORITETAS** (galima palikti demo):
9. Memento Pattern (jei nėra save/load)
10. Proxy Pattern (jei nėra resource management)
11. Interpreter Pattern (jei nėra console)

