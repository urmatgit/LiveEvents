# Конфигурация Mapster для SomeEvent

## Обзор

Была реализована правильная конфигурация Mapster для преобразования сущности SomeEvent в DTO SomeEventResponse с включением связанной информации из EventCatalog.

## Проблема

В оригинальной реализации:
- В `SomeEventResponse` было поле `EventCatalogName`, которого не было в ручной реализации `GetSomeEventHandler`
- `GetSomeEventHandler` создавал `SomeEventResponse` вручную без получения имени связанного каталога
- `SearchSomeEventsHandler` также не обеспечивал правильное заполнение поля `EventCatalogName` при поиске
- Это приводило к неполному ответу при получении данных

## Решение

### 1. Обновлена спецификация `GetSomeEventSpecs.cs`
- Использует `BaseSpecification<SomeEvent, SomeEventResponse>` для преобразования
- Включает связанную сущность `EventCatalog` через `.Include(p => p.EventCatalog)`
- Использует LINQ Select для проекции данных с включением имени каталога:
  ```csharp
  Query.Select(someEvent => new SomeEventResponse(
      someEvent.Id,
      someEvent.Name,
      // ... другие поля ...
      someEvent.EventCatalog != null ? someEvent.EventCatalog.Name : string.Empty
  ))
  ```

### 2. Обновлен обработчик `GetSomeEventHandler.cs`
- Заменена ручная сборка `SomeEventResponse` на использование спецификации
- Использует `GetSomeEventSpecs` для получения полной информации с включением имени каталога
- Сохранена функциональность кэширования

### 3. Обновлена спецификация `SearchSomeEventsSpecs.cs`
- Теперь наследуется от `EntitiesByPaginationFilterSpec<SomeEvent, SomeEventResponse>`
- Включает связанную сущность `EventCatalog` через `.Include(x=>x.EventCatalog)`
- Использует автоматическое преобразование Mapster для получения полного `SomeEventResponse`

### 4. Обновлен обработчик `SearchSomeEventsHandler.cs`
- Использует обновленную спецификацию `SearchSomeEventsSpecs`
- Обеспечивает полное заполнение `SomeEventResponse`, включая `EventCatalogName`
- Сохраняет функциональность пагинации и подсчета записей

## Технические детали

### Архитектурный подход
- Используется паттерн Specification из библиотеки Ardalis.Specification
- Используется встроенная функциональность Mapster через ProjectToType
- Поддерживается функциональность кэширования через ICacheService
- Поддерживается ленивая загрузка связанных данных

### Преимущества реализации
- Автоматическое преобразование сущности в DTO
- Включение связанных данных без дополнительных запросов
- Поддержка проекции только нужных полей (предотвращение N+1 проблемы)
- Сохранение производительности за счет проекции на уровне БД
- Типобезопасное преобразование
- Согласованность данных между различными эндпоинтами

## Результат

Теперь при любых запросах `SomeEvent`:
- Возвращается полный `SomeEventResponse` с заполненным полем `EventCatalogName`
- Используется эффективное преобразование через Mapster
- Сохраняется вся информация из связанной сущности
- Обеспечивается согласованность данных между различными эндпоинтами
- Поддерживаются все операции (получение по ID, поиск с фильтрацией и пагинацией)