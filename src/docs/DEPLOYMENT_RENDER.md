# Деплой на Render.com

## Обзор

Этот документ описывает процесс деплоя приложения LiveEvents на облачную платформу Render.com.

## Подготовка приложения

### 1. Dockerfile
Создан файл `Dockerfile` для контейнеризации ASP.NET Core приложения:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["api/server/Server.csproj", "api/server/"]
COPY ["api/framework/Infrastructure/Infrastructure.csproj", "api/framework/Infrastructure/"]
COPY ["api/framework/Core/Core.csproj", "api/framework/Core/"]
COPY ["api/modules/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "api/modules/Catalog/Catalog.Infrastructure/"]
COPY ["api/modules/Catalog/Catalog.Application/Catalog.Application.csproj", "api/modules/Catalog/Catalog.Application/"]
COPY ["api/modules/Catalog/Catalog.Domain/Catalog.Domain.csproj", "api/modules/Catalog/Catalog.Domain/"]
COPY ["Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "api/server/Server.csproj"
COPY . .
WORKDIR "/src/api/server"
RUN dotnet build "Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FSH.Starter.WebApi.Host.dll"]
```

### 2. Конфигурационный файл для Render
Создан файл `render.yaml` для автоматической настройки сервиса на Render:

```yaml
services:
  - type: web
    name: liveevents-api
    env: docker
    region: frankfurt  # или другой регион по вашему выбору
    plan: free
    branch: main
    healthCheckPath: /health
    envVars:
      - key: ASPNETCORE_ENVIRONMENT
        value: Production
      - key: DatabaseOptions__ConnectionString
        sync: false  # Это означает, что значение будет задано вручную в интерфейсе Render
      - key: ConnectionStrings__DefaultConnection
        sync: false
      - key: JwtOptions__Key
        sync: false
      - key: MailOptions__Password
        sync: false
      - key: Serilog__MinimumLevel__Default
        value: Information
```

## Процесс деплоя

### Шаг 1: Подготовьте репозиторий
1. Убедитесь, что все изменения закоммичены в репозиторий
2. Убедитесь, что Dockerfile и render.yaml находятся в корне репозитория
3. Загрузите изменения в удаленный репозиторий (GitHub, GitLab или Bitbucket)

### Шаг 2: Создайте аккаунт на Render.com
1. Перейдите на [https://render.com](https://render.com)
2. Создайте бесплатный аккаунт, используя GitHub, GitLab или Bitbucket

### Шаг 3: Создайте веб-сервис
1. Войдите в панель управления Render
2. Нажмите "New +" → "Web Service"
3. Выберите ваш репозиторий из списка
4. Render автоматически обнаружит Dockerfile и настроит сервис

### Шаг 4: Настройте переменные окружения
После создания сервиса, перейдите в настройки и добавьте следующие переменные окружения:

- `ASPNETCORE_ENVIRONMENT`: `Production`
- `DatabaseOptions__ConnectionString`: строка подключения к вашей базе данных (PostgreSQL)
- `JwtOptions__Key`: ключ JWT (должен быть 32 символа для AES-256)
- `MailOptions__Password`: пароль для почтового сервиса (если используется)
- `Serilog__MinimumLevel__Default`: `Information`

### Шаг 5: Настройте базу данных (если необходимо)
1. Если вы используете бесплатный план, Render предоставляет бесплатную PostgreSQL базу данных
2. Создайте новый "PostgreSQL" сервис на Render
3. Скопируйте строку подключения и используйте её в переменной `DatabaseOptions__ConnectionString`

### Шаг 6: Запустите деплой
1. Render автоматически начнет сборку и деплой приложения
2. Процесс может занять несколько минут
3. После завершения вы получите URL для доступа к приложению

## Особенности конфигурации

### Health Check
- Путь `/health` настроен для проверки состояния приложения
- Render будет использовать этот endpoint для мониторинга работоспособности

### Планы
- Используется бесплатный план, который подходит для тестирования и малонагруженных приложений
- Приложение может быть автоматически остановлено при отсутствии трафика (idle timeout)

### Регионы
- Выбран регион frankfurt, но вы можете выбрать ближайший к вам
- Доступные регионы: Oregon, Virginia, Frankfurt, Singapore

## Возможные проблемы и решения

### Проблема: Ошибка сборки Docker образа
**Решение**: Проверьте, что все .csproj файлы находятся в правильных местах и зависимости корректно указаны

### Проблема: Ошибка подключения к базе данных
**Решение**: Убедитесь, что строка подключения корректна и база данных доступна из интернета

### Проблема: Ошибка JWT ключа
**Решение**: Сгенерируйте правильный 32-символьный ключ для JWT аутентификации

## Масштабирование
- Render позволяет легко масштабировать приложение, изменив план в настройках
- Для высоконагруженных приложений рекомендуется использовать платные планы

## Безопасность
- Храните чувствительные данные (ключи, пароли) в переменных окружения, а не в коде
- Используйте HTTPS для защиты трафика
- Регулярно обновляйте зависимости и платформу