# Antrosios dalies šablonų pilnas statusas

## ✅ VISI 11 ŠABLONŲ REALIZUOTI

### a. Template Method ✅
- **Vieta**: `Templates/`
- **Realizacija**: 
  - `EntityUpdateTemplate` - abstrakti bazinė klasė
  - **3 sealed konkrečios klasės** (viršija 2+ reikalavimą):
    1. `BombUpdateTemplate` (sealed)
    2. `EnemyUpdateTemplate` (sealed)
    3. `PlayerUpdateTemplate` (sealed)
- **Demonstracija**: `TemplateMethodDemo.DemonstrateTemplateMethod()`

### b. Iterator ✅
- **Vieta**: `Iterators/`
- **Realizacija**: 
  - **3 skirtingos duomenų struktūros** (viršija 3+ reikalavimą):
    1. `BombCollection` - List<Bomb> (BombIterator)
    2. `EnemyCollection` - Dictionary<int, Enemy> (EnemyIterator)
    3. `TileGrid` - Tile[,] 2D masyvas (TileIterator)
- **Demonstracija**: `IteratorPatternDemo.DemonstrateIteratorPattern()`

### c. Flyweight ✅
- **Vieta**: `Flyweights/`
- **Realizacija**:
  - `TileFlyweight` - bendrinė būsena
  - `TileFlyweightFactory` - flyweight factory
  - `TileContext` - kontekstas su išorine būsena
- **Greitaveikos ir atminties matavimai**: `FlyweightPerformanceTest.RunPerformanceTest()`
- **Palyginimas**: Su flyweight ir be flyweight

### d. Composite ✅
- **Vieta**: `Composites/`
- **Realizacija**:
  - `IGameComponent` - komponento interfeisas
  - `GameEntityComponent` - leaf (atskiras komponentas)
  - `GameComposite` - composite (konteineris)
  - `SafeGameComposite` - saugus composite su lock funkcionalumu
- **Visibility režimai**: Public, Protected, Private
- **Safety režimas**: Locked/Unlocked
- **Skirtumas nuo Decorator**: `CompositeVsDecorator.md`

### e. State ✅
- **Vieta**: `States/`
- **Realizacija**: 
  - **5 būsenos** (viršija 4+ reikalavimą):
    1. `MenuState` (sealed)
    2. `PlayingState` (sealed)
    3. `PausedState` (sealed)
    4. `GameOverState` (sealed)
    5. `VictoryState` (sealed)
- **Būsenų diagrama**: Visos būsenos gali pereiti viena į kitą
- **Skirtumas nuo Strategy**: `StateVsStrategy.md`
- **Demonstracija**: `StatePatternDemo.DemonstrateStatePattern()`

### f. Proxy ✅
- **Vieta**: `Proxies/`
- **Realizacija**:
  - **3 proxy tipai**:
    1. `LazyLoadProxy` - delayed creation (vėlavęs kūrimas)
    2. `SecurityProxy` - security (saugumas, prieigos kontrolė)
    3. `LoggingProxy` - added functionality (papildoma funkcionalumas, logging)
  - `GameResource` - real subject
- **Greitaveikos ir atminties matavimai**: `ProxyPerformanceTest.RunPerformanceTest()`
- **Rezultatai**: Palyginimas su tiesioginiu prieiga

### g. Chain Of Responsibility ✅
- **Vieta**: `ChainOfResponsibility/`
- **Realizacija**:
  - **4 handler elementai** (viršija 4+ reikalavimą):
    1. `InputHandler` - įvesties apdorojimas
    2. `MovementHandler` - judėjimo apdorojimas
    3. `CollisionHandler` - susidūrimų apdorojimas
    4. `EventHandler` - įvykių apdorojimas
  - `RequestHandler` - bazinė handler klasė
  - `RequestChainBuilder` - grandinėlės kūrimas

### h. Visitor ✅
- **Vieta**: `Visitors/`
- **Realizacija**:
  - **3 visitor klasės** (viršija 3+ reikalavimą):
    1. `RenderVisitor` - renderinimo visitor
    2. `UpdateVisitor` - atnaujinimo visitor
    3. `CollisionVisitor` - susidūrimų detekcijos visitor
  - `VisitableEntity` - adapteris, kad esamos klasės būtų visitable
- **Demonstracija**: `VisitorPatternDemo.DemonstrateVisitorPattern()`

### i. Interpreter ✅
- **Vieta**: `Interpreters/`
- **Realizacija**:
  - `IExpression` - išraiškos interfeisas
  - Terminal expressions:
    - `MoveCommandExpression` - MOVE komanda
    - `BombCommandExpression` - BOMB komanda
    - `PauseCommandExpression` - PAUSE komanda
  - Non-terminal expression:
    - `CommandSequenceExpression` - komandų seka
  - `CommandParser` - konsolės komandų parseris
- **Naudojimas**: Konsolės komandų interpretavimas

### j. Mediator ✅
- **Vieta**: `Mediators/`
- **Realizacija**:
  - `GameMediator` - konkretus mediatorius
  - **3 participant klasės** (viršija 3+ reikalavimą):
    1. `PlayerManagerParticipant` - žaidėjo valdymas
    2. `BombManagerParticipant` - bombų valdymas
    3. `LevelManagerParticipant` - lygio valdymas
- **Tarpininkavimas**: Visi participantai komunikuoja per mediator
- **Skirtumas nuo Observer**: `MediatorVsObserver.md`

### k. Memento ✅
- **Vieta**: `Mementos/`
- **Realizacija**:
  - `GameMemento` - memento (saugo būseną)
  - `GameOriginator` - originator (kuria ir atkuria mementos)
  - `GameCaretaker` - caretaker (saugo mementos, bet negali pasiekti būsenos)
- **Saugumas**: 
  - Memento konstruktorius ir GetState() yra `internal` - tik Originator gali juos naudoti
  - Caretaker negali pasiekti memento būsenos
  - Kitos klasės negali gauti prieigos prie duomenų

## 📊 Reikalavimų įvykdymas

| Šablonas | Reikalavimas | Statusas | Pastabos |
|----------|--------------|----------|----------|
| Template Method | ≥2 sealed klasės | ✅ | 3 sealed klasės |
| Iterator | ≥3 struktūros | ✅ | List, Dictionary, 2D Array |
| Flyweight | Greitaveikos/atminties matavimai | ✅ | Performance test |
| Composite | Visibility/safety, skirtumas nuo Decorator | ✅ | 3 režimai, dokumentacija |
| State | ≥4 būsenos, skirtumas nuo Strategy | ✅ | 5 būsenos, dokumentacija |
| Proxy | Security/functionality/delayed, matavimai | ✅ | 3 tipai, performance test |
| Chain Of Responsibility | ≥4 elementai | ✅ | 4 handleriai |
| Visitor | ≥3 visitor klasės | ✅ | 3 visitor klasės |
| Interpreter | Konsolės komandos | ✅ | Command parser |
| Mediator | ≥3 klasės, skirtumas nuo Observer | ✅ | 3 participantai, dokumentacija |
| Memento | Saugus atstatymas | ✅ | Internal access, secure |

## 📝 Demonstracijos failai

- **Template Method**: `Templates/TemplateMethodDemo.cs`
- **Iterator**: `Iterators/IteratorPatternDemo.cs`
- **Flyweight**: `Flyweights/FlyweightPerformanceTest.cs`
- **Composite**: `Composites/GameComponent.cs` (naudojimo pavyzdžiai)
- **State**: `States/StatePatternDemo.cs`
- **Proxy**: `Proxies/ProxyPerformanceTest.cs`
- **Chain Of Responsibility**: `ChainOfResponsibility/RequestHandler.cs` (naudojimo pavyzdžiai)
- **Visitor**: `Visitors/VisitorPatternDemo.cs`
- **Interpreter**: `Interpreters/CommandExpression.cs` (naudojimo pavyzdžiai)
- **Mediator**: `Mediators/GameMediator.cs` (naudojimo pavyzdžiai)
- **Memento**: `Mementos/GameMemento.cs` (naudojimo pavyzdžiai)

## 🔍 Dokumentacija

- **State vs Strategy**: `States/StateVsStrategy.md`
- **Composite vs Decorator**: `Composites/CompositeVsDecorator.md`
- **Mediator vs Observer**: `Mediators/MediatorVsObserver.md`

## 📈 Projekto statistika

- **Pirmosios dalies šablonai**: 12 šablonų
- **Antrosios dalies šablonai**: 11 šablonų
- **Iš viso šablonų**: 23 šablonai
- **Iš viso klasės**: ~60+ klasės (viršija 40+ reikalavimą)

## ✅ Visi reikalavimai įvykdyti!

