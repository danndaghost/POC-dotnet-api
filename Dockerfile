# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY HelloWorldApi.csproj .
COPY src/HelloWorldApi.Domain/HelloWorldApi.Domain.csproj src/HelloWorldApi.Domain/
COPY src/HelloWorldApi.Application/HelloWorldApi.Application.csproj src/HelloWorldApi.Application/
COPY src/HelloWorldApi.Infrastructure/HelloWorldApi.Infrastructure.csproj src/HelloWorldApi.Infrastructure/
COPY src/HelloWorldApi.Interface/HelloWorldApi.Interface.csproj src/HelloWorldApi.Interface/

# Restore dependencies
RUN dotnet restore

# Copy source code
COPY . .

# Build the application
RUN dotnet build -c Release --no-restore

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release --no-build -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Create non-root user for security
RUN groupadd -r appgroup && useradd -r -g appgroup appuser

# Copy published application
COPY --from=publish /app/publish .

# Change ownership to non-root user
RUN chown -R appuser:appgroup /app
USER appuser

# Expose port
EXPOSE 8080

# Configure ASP.NET Core to listen on port 8080
ENV ASPNETCORE_URLS=http://+:8080

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/api/info/health || exit 1

# Entry point
ENTRYPOINT ["dotnet", "HelloWorldApi.dll"]