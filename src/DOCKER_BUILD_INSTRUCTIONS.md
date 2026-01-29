# Инструкции по сборке Docker образа

## Подготовка

Перед сборкой Docker образа убедитесь, что:

1. Установлен Docker Desktop или Docker Engine
2. Docker daemon запущен
3. Вы находитесь в корневой директории проекта (где расположен Dockerfile)

## Команда для сборки образа

Для сборки Docker образа выполните команду в терминале:

```bash
docker build -t liveevents-api .
```

## Дополнительные параметры сборки

### С указанием target:
```bash
docker build --target final -t liveevents-api .
```

### С указанием аргументов сборки:
```bash
docker build --build-arg BUILD_CONFIGURATION=Release -t liveevents-api .
```

### С подробным выводом:
```bash
docker build -t liveevents-api . --progress=plain
```

## Проверка образа

После сборки вы можете проверить, что образ создан:

```bash
docker images liveevents-api
```

## Запуск контейнера локально

Для тестирования собранного образа локально:

```bash
docker run -p 8080:80 liveevents-api
```

Приложение будет доступно по адресу http://localhost:8080

## Push в Docker Registry

Если вы хотите загрузить образ в Docker registry:

```bash
# Сначала залогиньтесь в нужный registry
docker login

# Затем пометьте образ
docker tag liveevents-api your-username/liveevents-api:latest

# И загрузите его
docker push your-username/liveevents-api:latest
```

## Возможные проблемы и решения

### Проблема: "no basic auth credentials"
**Решение**: Выполните `docker login` перед попыткой push

### Проблема: "out of space"
**Решение**: Очистите неиспользуемые образы командой `docker system prune`

### Проблема: Ошибка при сборке .NET проекта
**Решение**: Убедитесь, что все зависимости корректно скопированы и .csproj файлы находятся в правильных местах

## Заметки о Dockerfile

Наш Dockerfile использует многоступенчатую сборку:
1. Стадия `build` - восстанавливает зависимости и собирает приложение
2. Стадия `publish` - публикует приложение
3. Стадия `final` - создает финальный легковесный образ на базе ASP.NET Core runtime

Это позволяет уменьшить размер финального образа и улучшить безопасность.