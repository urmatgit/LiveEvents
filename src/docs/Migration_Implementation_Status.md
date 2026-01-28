# Статус реализации миграций

## Выполненные изменения

### 1. Добавление связи в доменные классы
- В `EventCatalog` добавлено свойство `ICollection<SomeEvent> SomeEvents { get; protected set; } = new List<SomeEvent>();`
- Обновлены конфигурации EF Core для отражения связи один ко многим

### 2. Обновление конфигураций
- `SomeEventConfiguration.cs` - обновлена связь с EventCatalog
- `EventCatalogConfiguration.cs` - добавлена настройка связи с SomeEvents

### 3. Создание миграций
- **PostgreSQL**: успешно создана миграция `20260128120139_AddSomeEventCollectionToEventCatalog.cs` и применена к базе данных
- **MSSQL**: создана миграция `20260128120800_AddSomeEventCollectionToEventCatalog.cs` вручную с учетом особенностей SQL Server

### 4. Структура таблицы
Миграция создает таблицу `SomeEvents` со следующими полями:
- `Id` - уникальный идентификатор
- `Name` - название события
- `Description` - описание события
- `OrganizationId` - ID организации
- `MinParticipant`, `MaxParticipant` - минимальное и максимальное количество участников
- `Durations` - продолжительность
- `Price` - цена
- `EventDate` - дата события
- `EventCatalogId` - внешний ключ к EventCatalog
- Аудит поля (TenantId, Created, CreatedBy, LastModified, LastModifiedBy, Deleted, DeletedBy)

### 5. Связи
- Один `EventCatalog` может содержать много `SomeEvent`
- При удалении `EventCatalog` все связанные `SomeEvent` удаляются каскадно

## Статус
- ✅ Доменная модель обновлена
- ✅ Конфигурации EF Core обновлены
- ✅ Миграции созданы для обоих провайдеров БД
- ✅ База данных PostgreSQL успешно обновлена
- ✅ База данных MSSQL подготовлена для обновления (миграционный файл создан)

## Применение миграции к MSSQL
Для применения миграции к MSSQL выполните команду:
```bash
dotnet ef database update --project api/migrations/MSSQL/MSSQL.csproj --startup-project api/server/Server.csproj --context CatalogDbContext
```

## Использование
Теперь можно использовать связь между сущностями:
```csharp
var eventCatalog = await context.EventCatalogs
    .Include(x => x.SomeEvents)
    .FirstOrDefaultAsync(x => x.Id == catalogId);

foreach(var someEvent in eventCatalog.SomeEvents)
{
    Console.WriteLine($"Событие: {someEvent.Name}");
}