# Antrosios dalies šablonų statusas (Part 2 Patterns Status)

## ✅ Realizuoti šablonai

### 1. **State Pattern** ✅
- **Vieta**: `States/` direktorija
- **Realizacija**: 
  - `IGameState` - būsenų interfeisas
  - `GameStateContext` - kontekstas, valdantis būsenas
  - **5 konkrečių būsenų** (viršija 4+ reikalavimą):
    1. `MenuState` (sealed)
    2. `PlayingState` (sealed)
    3. `PausedState` (sealed)
    4. `GameOverState` (sealed)
    5. `VictoryState` (sealed)
- **Būsenų diagrama**: Visos būsenos gali pereiti viena į kitą per kontekstą
- **Skirtumas nuo Strategy**: `States/StateVsStrategy.md`
- **Demonstracija**: `StatePatternDemo.DemonstrateStatePattern()`

### 2. **Iterator Pattern** ✅
- **Vieta**: `Iterators/` direktorija
- **Realizacija**:
  - `IIterator<T>` - iterator interfeisas
  - `IIterable<T>` - iteruojamo objekto interfeisas
  - **3 skirtingos duomenų struktūros** (viršija 3+ reikalavimą):
    1. `BombCollection` - naudoja `List<Bomb>` (BombIterator)
    2. `EnemyCollection` - naudoja `Dictionary<int, Enemy>` (EnemyIterator)
    3. `TileGrid` - naudoja `Tile[,]` 2D masyvą (TileIterator)
- **Kiekviena struktūra turi savo iterator klasę**:
  - `BombIterator` - iteruoja per List
  - `EnemyIterator` - iteruoja per Dictionary reikšmes
  - `TileIterator` - iteruoja per 2D masyvą (praleidžia null reikšmes)
- **Demonstracija**: `IteratorPatternDemo.DemonstrateIteratorPattern()`

### 3. **Template Method Pattern** ✅
- **Vieta**: `Templates/` direktorija
- **Realizacija**:
  - `EntityUpdateTemplate` - abstrakti šablono klasė
  - **3 sealed konkrečios klasės** (viršija 2+ reikalavimą):
    1. `BombUpdateTemplate` (sealed)
    2. `EnemyUpdateTemplate` (sealed)
    3. `PlayerUpdateTemplate` (sealed)
- **Template metodas**: `UpdateEntity()` - apibrėžia algoritmo skeletą
- **Hook metodai**: `ValidateEntity()`, `HandleInvalidEntity()`, `PostUpdate()`
- **Abstraktus metodas**: `PerformUpdate()` - turi būti realizuotas subklasėse
- **Demonstracija**: `TemplateMethodDemo.DemonstrateTemplateMethod()`

## 📊 Reikalavimų įvykdymas

| Šablonas | Reikalavimas | Statusas | Pastabos |
|----------|--------------|----------|----------|
| State | ≥4 būsenos, paaiškinti skirtumą nuo Strategy | ✅ | 5 būsenos, dokumentacija `StateVsStrategy.md` |
| Iterator | ≥3 skirtingos duomenų struktūros | ✅ | List, Dictionary, 2D Array |
| Template Method | ≥2 sealed konkrečios klasės | ✅ | 3 sealed klasės |

## 📝 Demonstracijos failai

- **State**: `States/StatePatternDemo.cs`
- **Iterator**: `Iterators/IteratorPatternDemo.cs`
- **Template Method**: `Templates/TemplateMethodDemo.cs`

## 🔍 Dokumentacija

- **State vs Strategy**: `States/StateVsStrategy.md` - išsamus palyginimas su pavyzdžiais

## 🎯 Kitos galimos realizacijos

Jei reikia daugiau šablonų, galima realizuoti:
- **Composite** - žaidimo objektų hierarchija (pvz., level komponentai)
- **Flyweight** - optimizuoti tile/particle objektus su greitaveikos matavimais
- **Proxy** - saugus prieiga prie žaidimo resursų su greitaveikos matavimais
- **Chain of Responsibility** - žaidimo įvykių apdorojimo grandinėlė (≥4 elementai)
- **Visitor** - žaidimo objektų apdorojimas (≥3 visitor klasės)
- **Interpreter** - konsolės komandų interpretavimas
- **Mediator** - komunikacija tarp žaidimo komponentų (≥3 klasės)
- **Memento** - saugus žaidimo būsenos išsaugojimas/atstatymas

## 📈 Klasės skaičius

Dabar projekte yra:
- Pirmosios dalies: ~30+ klasės
- Antrosios dalies pridėta: ~15+ klasės
- **Iš viso: ~45+ klasės** (viršija 40+ reikalavimą)

