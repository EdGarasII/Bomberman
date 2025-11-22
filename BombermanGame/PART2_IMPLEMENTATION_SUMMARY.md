# Antrosios dalies realizacijos santrauka

## ✅ Realizuoti 3 šablonai

### 1. State Pattern (Būsenų šablonas)
**Reikalavimas**: ≥4 būsenos, paaiškinti skirtumą nuo Strategy

**Realizacija**:
- ✅ 5 būsenos (viršija reikalavimą):
  1. `MenuState` - meniu būsena
  2. `PlayingState` - žaidimo būsena
  3. `PausedState` - pauzės būsena
  4. `GameOverState` - žaidimo pabaigos būsena
  5. `VictoryState` - pergalės būsena
- ✅ Visos klasės `sealed`
- ✅ Dokumentacija: `States/StateVsStrategy.md` - išsamus palyginimas su Strategy šablonu

**Failai**:
- `States/IGameState.cs` - būsenų interfeisas
- `States/GameStateContext.cs` - kontekstas
- `States/MenuState.cs`, `PlayingState.cs`, `PausedState.cs`, `GameOverState.cs`, `VictoryState.cs`
- `States/StatePatternDemo.cs` - demonstracija
- `States/StateVsStrategy.md` - dokumentacija

### 2. Iterator Pattern (Iteratoriaus šablonas)
**Reikalavimas**: Iteruoti per ≥3 skirtingas duomenų struktūras

**Realizacija**:
- ✅ 3 skirtingos duomenų struktūros:
  1. `BombCollection` - naudoja `List<Bomb>` (BombIterator)
  2. `EnemyCollection` - naudoja `Dictionary<int, Enemy>` (EnemyIterator)
  3. `TileGrid` - naudoja `Tile[,]` 2D masyvą (TileIterator)
- ✅ Kiekviena struktūra turi savo iterator klasę
- ✅ Visi iteratoriai realizuoja `IIterator<T>` interfeisą

**Failai**:
- `Iterators/IIterator.cs` - iterator interfeisas
- `Iterators/IIterable.cs` - iteruojamo objekto interfeisas
- `Iterators/BombCollection.cs`, `Iterators/EnemyCollection.cs`, `Iterators/TileGrid.cs`
- `Iterators/IteratorPatternDemo.cs` - demonstracija

### 3. Template Method Pattern (Šablono metodo šablonas)
**Reikalavimas**: ≥2 sealed konkrečios klasės

**Realizacija**:
- ✅ 3 sealed konkrečios klasės (viršija reikalavimą):
  1. `BombUpdateTemplate` - bombų atnaujinimo šablonas
  2. `EnemyUpdateTemplate` - priešų atnaujinimo šablonas
  3. `PlayerUpdateTemplate` - žaidėjo atnaujinimo šablonas
- ✅ Abstrakti bazinė klasė `EntityUpdateTemplate` su template metodu
- ✅ Hook metodai: `ValidateEntity()`, `HandleInvalidEntity()`, `PostUpdate()`
- ✅ Abstraktus metodas: `PerformUpdate()` - turi būti realizuotas subklasėse

**Failai**:
- `Templates/EntityUpdateTemplate.cs` - abstrakti šablono klasė
- `Templates/BombUpdateTemplate.cs`, `EnemyUpdateTemplate.cs`, `PlayerUpdateTemplate.cs`
- `Templates/TemplateMethodDemo.cs` - demonstracija

## 📊 Reikalavimų įvykdymas

| Kriterijus | Reikalavimas | Statusas |
|------------|--------------|----------|
| Realizuoti šablonai | ≥3 šablonai | ✅ 3 šablonai |
| State Pattern | ≥4 būsenos, paaiškinti nuo Strategy | ✅ 5 būsenos, dokumentacija |
| Iterator Pattern | ≥3 skirtingos struktūros | ✅ 3 struktūros (List, Dictionary, 2D Array) |
| Template Method | ≥2 sealed klasės | ✅ 3 sealed klasės |

## 🎯 Kaip naudoti

### State Pattern demonstracija:
```csharp
using BombermanGame.States;

StatePatternDemo.DemonstrateStatePattern();
```

### Iterator Pattern demonstracija:
```csharp
using BombermanGame.Iterators;

IteratorPatternDemo.DemonstrateIteratorPattern();
```

### Template Method demonstracija:
```csharp
using BombermanGame.Templates;

TemplateMethodDemo.DemonstrateTemplateMethod();
```

## 📈 Projekto statistika

- **Pirmosios dalies šablonai**: 12 šablonų
- **Antrosios dalies šablonai**: 3 šablonai
- **Iš viso klasės**: ~45+ klasės (viršija 40+ reikalavimą)

## 📝 Pastabos

- Visi šablonai integruoti su esamu kodu
- Kompiliavimas sėkmingas (0 klaidų)
- Demonstracijos klasės paruoštos naudojimui
- Dokumentacija apie State vs Strategy pateikta

