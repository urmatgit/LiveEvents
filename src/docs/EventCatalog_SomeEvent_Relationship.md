# Отношение между EventCatalog и SomeEvent

## Описание изменений

Было добавлено отношение один ко многим между `EventCatalog` и `SomeEvent`:

- `EventCatalog` теперь содержит коллекцию `SomeEvents`
- `SomeEvent` содержит ссылку на `EventCatalog` через `EventCatalogId`

## Классы, которые были изменены

### 1. `EventCatalog.cs`
- Добавлено свойство `ICollection<SomeEvent> SomeEvents { get; protected set; } = new List<SomeEvent>();`
- Это позволяет получить доступ ко всем `SomeEvent`, связанным с конкретным `EventCatalog`

### 2. `SomeEvent.cs` (уже существовало)
- Свойство `EventCatalogId` - внешний ключ для связи
- Свойство `EventCatalog` - навигационное свойство

### 3. `SomeEventConfiguration.cs`
- Обновлена конфигурация связи: `.WithMany(x => x.SomeEvents)` вместо `.WithMany()`
- Установлен каскадный режим удаления (`DeleteBehavior.Cascade`)

### 4. `EventCatalogConfiguration.cs`
- Добавлена настройка связи: `.HasMany(x => x.SomeEvents).WithOne(x => x.EventCatalog)...`

## Тип отношения
- `EventCatalog` → `SomeEvent`: один ко многим
- Один каталог событий может содержать множество конкретных событий
- При удалении `EventCatalog` все связанные `SomeEvent` также будут удалены (каскадное удаление)

## Пример использования

```csharp
// Получение всех SomeEvent для конкретного EventCatalog
var eventCatalog = await dbContext.EventCatalogs
    .Include(x => x.SomeEvents)
    .FirstOrDefaultAsync(x => x.Id == catalogId);

foreach(var someEvent in eventCatalog.SomeEvents)
{
    Console.WriteLine($"Событие: {someEvent.Name}");
}
```

## Потенциальные сценарии использования
- Группировка событий по категориям (например, "Конференции", "Обучающие семинары", "Выставки")
- Фильтрация событий по типу каталога
- Управление группами связанных событий