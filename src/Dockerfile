FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
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