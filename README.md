# HelloWorld API - ASP.NET Core 8 con Arquitectura Hexagonal

POC (Proof of Concept) de una API REST desarrollada con ASP.NET Core 8 implementando arquitectura hexagonal (Ports and Adapters pattern).

## 🏗️ Arquitectura

Este proyecto implementa **Arquitectura Hexagonal** con las siguientes capas:

```
src/
├── HelloWorldApi.Domain/          # Capa de Dominio
│   ├── Entities/                  # Entidades del negocio
│   ├── ValueObjects/              # Objetos de valor
│   └── Repositories/              # Interfaces de repositorios
├── HelloWorldApi.Application/     # Capa de Aplicación
│   ├── DTOs/                      # Data Transfer Objects
│   ├── UseCases/                  # Casos de uso del negocio
│   └── Services/                  # Servicios de aplicación
├── HelloWorldApi.Infrastructure/  # Capa de Infraestructura
│   ├── Repositories/              # Implementaciones de repositorios
│   └── Services/                  # Servicios externos
└── HelloWorldApi.Interface/       # Capa de Interface
    ├── Controllers/               # Controladores API
    └── Middleware/                # Middlewares personalizados
```

## 🚀 Características

- ✅ **ASP.NET Core 8** con .NET 8
- ✅ **Arquitectura Hexagonal** (Ports & Adapters)
- ✅ **Clean Architecture** con separación de responsabilidades
- ✅ **RESTful API** con Swagger/OpenAPI
- ✅ **Docker** y **Docker Compose** ready
- ✅ **Health Checks** integrados
- ✅ **Global Exception Handling**
- ✅ **CORS** configurado
- ✅ **Logging** estructurado
- ✅ **Nginx** como reverse proxy (opcional)

## 🛠️ Tecnologías

- **Runtime**: .NET 8
- **Framework**: ASP.NET Core 8
- **Documentación**: Swagger/OpenAPI
- **Contenedores**: Docker & Docker Compose
- **Proxy**: Nginx (opcional)

## 📋 Prerequisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

## 🚀 Instalación y Ejecución

### Opción 1: Docker Compose (Recomendado)

```bash
# Clonar y navegar al directorio
cd /Proyectos/POC/API.NET

# Ejecutar en desarrollo
docker-compose -f docker-compose.dev.yml up --build

# O ejecutar en producción
docker-compose up --build

# Con Nginx (producción)
docker-compose --profile nginx up --build
```

### Opción 2: .NET CLI (Desarrollo local)

```bash
# Restaurar dependencias
dotnet restore

# Ejecutar la aplicación
dotnet run

# O ejecutar en modo desarrollo
dotnet watch run
```

## 🌐 Endpoints Disponibles

La API estará disponible en:

- **Desarrollo local**: `http://localhost:5000`
- **Docker**: `http://localhost:5000`
- **Swagger UI**: `http://localhost:5000/swagger`

### Endpoints principales:

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/` | Página de bienvenida |
| `GET` | `/api/helloworld` | Obtener mensaje Hello World |
| `GET` | `/api/helloworld/messages` | Obtener todos los mensajes |
| `POST` | `/api/helloworld/messages` | Crear nuevo mensaje |
| `GET` | `/api/info` | Información de la API |
| `GET` | `/api/info/health` | Health check |

### Ejemplos de uso:

```bash
# Hello World básico
curl http://localhost:5000/api/helloworld

# Health check
curl http://localhost:5000/api/info/health

# Crear mensaje
curl -X POST http://localhost:5000/api/helloworld/messages \
  -H "Content-Type: application/json" \
  -d '{"content": "Mi mensaje personalizado", "author": "Usuario"}'

# Obtener todos los mensajes
curl http://localhost:5000/api/helloworld/messages
```

## 🧪 Testing

```bash
# Ejecutar tests unitarios (cuando se implementen)
dotnet test

# Test de la API en funcionamiento
curl http://localhost:5000/api/info/health
```

## 📁 Estructura del Proyecto

```
HelloWorldApi/
├── src/
│   ├── HelloWorldApi.Domain/
│   │   ├── Entities/
│   │   │   └── Message.cs
│   │   ├── ValueObjects/
│   │   │   └── MessageType.cs
│   │   └── Repositories/
│   │       └── IMessageRepository.cs
│   ├── HelloWorldApi.Application/
│   │   ├── DTOs/
│   │   │   └── MessageDto.cs
│   │   ├── UseCases/
│   │   │   ├── IMessageUseCases.cs
│   │   │   └── MessageUseCases.cs
│   │   └── Services/
│   │       └── ApplicationServiceExtensions.cs
│   ├── HelloWorldApi.Infrastructure/
│   │   ├── Repositories/
│   │   │   └── InMemoryMessageRepository.cs
│   │   └── Services/
│   │       └── InfrastructureServiceExtensions.cs
│   └── HelloWorldApi.Interface/
│       ├── Controllers/
│       │   ├── HelloWorldController.cs
│       │   └── InfoController.cs
│       └── Middleware/
│           └── GlobalExceptionMiddleware.cs
├── nginx/
│   └── nginx.conf
├── tests/
├── docker-compose.yml
├── docker-compose.dev.yml
├── Dockerfile
└── Program.cs
```

## 🔧 Configuración

### Variables de Entorno

```bash
# Entorno de ejecución
ASPNETCORE_ENVIRONMENT=Development|Production

# URL de escucha
ASPNETCORE_URLS=http://+:8080

# Zona horaria
TZ=America/Santiago
```

### Docker Compose Profiles

- **Default**: Solo la API
- **nginx**: API + Nginx como reverse proxy

## 🔄 CI/CD

El proyecto está preparado para implementar CI/CD con:

- Docker multi-stage builds optimizados
- Health checks para monitoreo
- Configuración de producción lista
- Separación de entornos (dev/prod)

## 📊 Monitoreo

- **Health Check**: `/api/info/health`
- **API Info**: `/api/info`
- **Swagger Docs**: `/swagger`

## 🤝 Contribución

Este es un proyecto POC. Para contribuir:

1. Fork del proyecto
2. Crear branch para features
3. Implementar tests
4. Crear Pull Request

## 📄 Licencia

Este proyecto es una POC para fines educativos y de demostración.

## 📞 Soporte

Para consultas sobre este POC, contacta al equipo de desarrollo.

---

**Nota**: Este es un proyecto de demostración (POC) que implementa las mejores prácticas de arquitectura hexagonal con ASP.NET Core 8.