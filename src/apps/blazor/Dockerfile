# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src

# Copy all project files
COPY . .

# Restore packages with warnings suppressed
WORKDIR /src/apps/blazor/client
RUN dotnet restore --disable-parallel

# Build the application
RUN dotnet build -c Release --no-restore

# Publish the Blazor WebAssembly application
RUN dotnet publish -c Release -o /app/publish --no-build

# Use nginx to serve the static files
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Copy published output from build stage
COPY --from=build-env /app/publish/wwwroot .

# Copy custom nginx configuration
COPY apps/blazor/nginx.conf /etc/nginx/nginx.conf

# Expose port 80
EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]