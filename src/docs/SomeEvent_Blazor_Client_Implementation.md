# Реализация клиентского интерфейса SomeEvent для Blazor

## Обзор

Был создан клиентский интерфейс для управления SomeEvent в Blazor-приложении, основанный на существующем компоненте Products.

## Созданные файлы

### 1. SomeEventViewModel.cs
- Расположение: `apps/blazor/client/Pages/Catalog/SomeEventViewModel.cs`
- Наследуется от `UpdateSomeEventCommand`
- Используется как ViewModel для формы редактирования

### 2. SomeEvents.razor
- Расположение: `apps/blazor/client/Pages/Catalog/SomeEvents.razor`
- Основной компонент страницы для управления SomeEvent
- Использует EntityTable для отображения данных
- Содержит поля для всех свойств SomeEvent
- Включает расширенный поиск по EventCatalog

### 3. SomeEvents.razor.cs
- Расположение: `apps/blazor/client/Pages/Catalog/SomeEvents.razor.cs`
- Код-бихайнд для компонента
- Настройка контекста таблицы
- Загрузка списка EventCatalog для выпадающего списка
- Обработка операций CRUD

### 4. Изменения в NavMenu.razor
- Добавлен пункт меню "SomeEvents" в группу "Catalog"
- Ссылка на `/catalog/someevents`
- Иконка `@Icons.Material.Filled.EventAvailable`

## Функциональность

### Основные возможности
- Отображение списка SomeEvent в таблице
- Создание новых SomeEvent
- Редактирование существующих SomeEvent
- Удаление SomeEvent
- Поиск и фильтрация

### Поля формы
- Name: текстовое поле
- Description: текстовое поле
- OrganizationId: текстовое поле для GUID
- MinParticipant: числовое поле
- MaxParticipant: числовое поле
- Durations: числовое поле
- Price: числовое поле
- EventDate: DatePicker
- EventCatalogId: выпадающий список с выбором EventCatalog

### Расширенный поиск
- Фильтрация по EventCatalog
- Выбор из всех доступных EventCatalog

## Интеграция с API

Компонент интегрирован с существующими API-эндпоинтами:
- `SearchSomeEventsEndpointAsync` - для получения списка
- `CreateSomeEventEndpointAsync` - для создания
- `UpdateSomeEventEndpointAsync` - для обновления
- `DeleteSomeEventEndpointAsync` - для удаления
- `SearchEventCatalogsEndpointAsync` - для получения списка каталогов

## Авторизация

Используются следующие разрешения:
- `FshResources.SomeEvents` - для основных операций
- Поддержка стандартных действий: View, Create, Update, Delete, Export

## Структура данных

Компонент работает с:
- `SomeEventResponse` - для отображения данных
- `SomeEventViewModel` - для формы редактирования
- `EventCatalogResponse` - для выбора каталога событий

## Навигация

Страница доступна по адресу `/catalog/someevents` и интегрирована в меню навигации в разделе Catalog.