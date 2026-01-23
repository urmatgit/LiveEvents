# SomeEvent Module

Модуль SomeEvent представляет собой сущность события с полной CRUD-функциональностью, аналогичной EventCatalog.

## Структура модуля

### Доменная модель
- `SomeEvent.cs` - основная доменная сущность
- `Events/` - доменные события (SomeEventCreated, SomeEventUpdated)
- `Exceptions/` - исключения (SomeEventNotFoundException)

### Инфраструктура
- `Configurations/SomeEventConfiguration.cs` - конфигурация EF Core

### Прикладной слой (Application)
- `Create/v1/` - создание SomeEvent (команда, обработчик, валидатор, ответ)
- `Get/v1/` - получение SomeEvent (запрос, обработчик, ответ)
- `Update/v1/` - обновление SomeEvent (команда, обработчик, валидатор, ответ)
- `Delete/v1/` - удаление SomeEvent (команда, обработчик, валидатор, ответ)
- `Search/v1/` - поиск SomeEvent (команда, обработчик, валидатор, спецификация)
- `GetByName/` - спецификация и обработчик для поиска по имени

## Свойства SomeEvent

- `Id` - уникальный идентификатор
- `Name` - название события
- `Description` - описание события
- `OrganizationId` - идентификатор организации
- `MinParticipant` - минимальное количество участников (должно быть больше 0)
- `MaxParticipant` - максимальное количество участников (должно быть больше 0 и не меньше минимального)
- `Durations` - продолжительность события
- `Price` - цена события
- `EventDate` - дата и время события
- `EventCatalogId` - идентификатор связанного каталога событий
- `EventCatalog` - виртуальное свойство для связи с каталогом событий

## Ограничения

- Минимальное количество участников (`MinParticipant`) должно быть не менее 1
- Максимальное количество участников (`MaxParticipant`) должно быть не менее 1
- Максимальное количество участников должно быть не меньше минимального
- Все операции создания, обновления и удаления логируются
- Валидация бизнес-правил осуществляется с помощью проверок в коде доменной сущности

## Использование

Модуль полностью следует шаблону проектирования Clean Architecture и может использоваться через MediatR команды и запросы.

## API Endpoints

Для взаимодействия с сущностью SomeEvent доступны следующие HTTP endpoints:

- `POST /api/v1/someevents` - создание нового SomeEvent
- `GET /api/v1/someevents/{id}` - получение SomeEvent по ID
- `GET /api/v1/someevents` - получение списка SomeEvent
- `PUT /api/v1/someevents/{id}` - обновление SomeEvent
- `DELETE /api/v1/someevents/{id}` - удаление SomeEvent

Каждый endpoint требует соответствующих разрешений (Permissions.SomeEvents.Create, View, Search, Update, Delete).

## Миграции

Для добавления сущности SomeEvent в базу данных были созданы миграции:
- `api/migrations/PostgreSQL/Migrations/20260123142200_AddSomeEventToCatalogDb.cs` для PostgreSQL
- `api/migrations/MSSQL/Migrations/20260123142200_AddSomeEventToCatalogDb.cs` для MSSQL

Также были обновлены ModelSnapshot файлы для отражения новой структуры базы данных.