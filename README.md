# HelloWorld API - ASP.NET Core 8 con Arquitectura Hexagonal

POC (Proof of Concept) de una API REST desarrollada con ASP.NET Core 8 implementando arquitectura hexagonal (Ports and Adapters pattern) con integración a PostgreSQL.

## 🏗️ Arquitectura

Este proyecto implementa **Arquitectura Hexagonal** organizada en un solo proyecto con carpetas por capas:

```
HelloWorldApi/
├── Domain/                        # Capa de Dominio
│   ├── Entities/                  # Entidades del negocio
│   │   ├── Message.cs             # Entidad Message (en memoria)
│   │   └── GenGeneral.cs          # Entidad para tabla sisa.gen_general
│   └── Repositories/              # Interfaces de repositorios (Ports)
│       ├── IMessageRepository.cs
│       └── IGenGeneralRepository.cs
├── Application/                   # Capa de Aplicación
│   ├── DTOs/                      # Data Transfer Objects
│   │   ├── MessageDto.cs
│   │   └── GenGeneralDto.cs
│   └── UseCases/                  # Casos de uso del negocio
│       ├── MessageUseCases.cs
│       └── GenGeneralUseCases.cs
├── Infrastructure/                # Capa de Infraestructura (Adapters)
│   ├── Data/
│   │   └── ApplicationDbContext.cs # DbContext para Entity Framework
│   └── Repositories/              # Implementaciones de repositorios
│       ├── InMemoryMessageRepository.cs
│       └── PostgreSqlGenGeneralRepository.cs
├── Controllers/                   # Capa de Interface (REST Controllers)
│   ├── HelloWorldController.cs
│   ├── InfoController.cs
│   └── GenGeneralController.cs
├── Middleware/                    # Middlewares personalizados
│   └── GlobalExceptionMiddleware.cs
└── Migrations/                    # Migraciones de Entity Framework
```

## 🚀 Características

- ✅ **ASP.NET Core 8** con .NET 8
- ✅ **Arquitectura Hexagonal** (Ports & Adapters)
- ✅ **Entity Framework Core** con PostgreSQL
- ✅ **Repositorio In-Memory** para desarrollo/pruebas
- ✅ **RESTful API** con Swagger/OpenAPI
- ✅ **Docker** y **Docker Compose** ready
- ✅ **Health Checks** integrados
- ✅ **Global Exception Handling**
- ✅ **CORS** configurado
- ✅ **Database Migrations** con Entity Framework
- ✅ **Nginx** como reverse proxy (opcional)

## 🛠️ Tecnologías

- **Runtime**: .NET 8
- **Framework**: ASP.NET Core 8
- **Base de Datos**: PostgreSQL con Entity Framework Core
- **ORM**: Entity Framework Core 9.0
- **PostgreSQL Provider**: Npgsql.EntityFrameworkCore.PostgreSQL
- **Documentación**: Swagger/OpenAPI
- **Contenedores**: Docker & Docker Compose
- **Proxy**: Nginx (opcional)

## 📋 Prerequisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) (para base de datos)
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [Entity Framework CLI Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

## 🚀 Instalación y Ejecución

### Configuración de Base de Datos PostgreSQL

```bash
# 1. Crear base de datos y esquema
createdb sisa
psql -d sisa -c "CREATE SCHEMA IF NOT EXISTS sisa;"

# 2. Configurar credenciales en .env
DB_HOST=localhost
DB_PORT=5432
DB_NAME=sisa
DB_USER=postgres
DB_PASSWORD=tu_password
```

```bash
# 3. Configurar appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=sisa;Username=postgres;Password=tu_password;"
  }
}
```

### Opción 1: .NET CLI (Desarrollo local)

```bash
# Navegar al directorio del proyecto
cd /home/daniel/Proyectos/POC/API.NET

# Restaurar dependencias
dotnet restore

# Instalar herramientas de Entity Framework (si no está instalado)
dotnet tool install --global dotnet-ef

# Aplicar migraciones a la base de datos
dotnet ef database update

# Ejecutar la aplicación
ASPNETCORE_ENVIRONMENT=Development dotnet run

# O ejecutar en modo desarrollo con watch
dotnet watch run
```

### Opción 2: Docker Compose

```bash
# Ejecutar en desarrollo
docker-compose -f docker-compose.dev.yml up --build

# O ejecutar en producción
docker-compose up --build

# Con Nginx (producción)
docker-compose --profile nginx up --build
```

## 🌐 Endpoints Disponibles

La API estará disponible en:

- **Desarrollo local**: `http://localhost:5000`
- **Docker**: `http://localhost:5000`
- **Swagger UI**: `http://localhost:5000/swagger` (modo Development)

### Endpoints principales:

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/` | Página de bienvenida de la API |
| `GET` | `/api/info/health` | Health check |

### Messages (Repositorio en memoria):

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/helloworld` | Obtener mensaje Hello World |
| `GET` | `/api/helloworld/messages` | Obtener todos los mensajes |
| `POST` | `/api/helloworld/messages` | Crear nuevo mensaje |
| `GET` | `/api/helloworld/messages/{id}` | Obtener mensaje por ID |

### Gen General (PostgreSQL - Tabla sisa.gen_general):

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/gengeneral` | Obtener todos los registros |
| `GET` | `/api/gengeneral/activos` | Obtener solo registros activos |
| `GET` | `/api/gengeneral/{id}` | Obtener registro por ID |
| `GET` | `/api/gengeneral/codigo/{codigo}` | Obtener registro por código |
| `POST` | `/api/gengeneral` | Crear nuevo registro |
| `PUT` | `/api/gengeneral/{id}` | Actualizar registro existente |
| `DELETE` | `/api/gengeneral/{id}` | Eliminar registro |
| `HEAD` | `/api/gengeneral/{id}` | Verificar si existe registro |

### Ejemplos de uso:

```bash
# Health check
curl http://localhost:5000/api/info/health

# Hello World básico
curl http://localhost:5000/api/helloworld

# Crear mensaje en memoria
curl -X POST http://localhost:5000/api/helloworld/messages \
  -H "Content-Type: application/json" \
  -d '{"content": "Mi mensaje personalizado"}'

# Obtener todos los registros de gen_general
curl http://localhost:5000/api/gengeneral

# Crear registro en PostgreSQL
curl -X POST http://localhost:5000/api/gengeneral \
  -H "Content-Type: application/json" \
  -d '{
    "codigo": "TEST_001",
    "nombre": "Registro de Prueba",
    "descripcion": "Este es un registro de prueba",
    "valor": "valor_ejemplo",
    "activo": true
  }'

# Obtener solo registros activos
curl http://localhost:5000/api/gengeneral/activos

# Obtener registro por código
curl http://localhost:5000/api/gengeneral/codigo/TEST_001
```

## 🧪 Testing

### Tests de API

```bash
# Test de health check
curl http://localhost:5000/api/info/health

# Test de endpoints básicos
curl http://localhost:5000/
curl http://localhost:5000/api/helloworld

# Test de repositorio en memoria
curl -X POST http://localhost:5000/api/helloworld/messages \
  -H "Content-Type: application/json" \
  -d '{"content": "Test message"}'

# Test de PostgreSQL (requiere BD configurada)
curl http://localhost:5000/api/gengeneral/activos
```

### Tests Unitarios

```bash
# Ejecutar tests unitarios (cuando se implementen)
dotnet test

# Ejecutar tests con cobertura
dotnet test --collect:"XPlat Code Coverage"
```

### Troubleshooting

```bash
# Verificar compilación
dotnet build

# Verificar configuración EF Core
dotnet ef dbcontext info

# Verificar migraciones pendientes
dotnet ef migrations list

# Verificar logs de la aplicación
tail -f api.log
```

## 📁 Estructura del Proyecto Real

```
HelloWorldApi/
├── Application/                           # Capa de Aplicación
│   ├── DTOs/
│   │   ├── MessageDto.cs                 # DTOs para Messages
│   │   └── GenGeneralDto.cs              # DTOs para Gen General
│   └── UseCases/
│       ├── MessageUseCases.cs            # Casos de uso Messages
│       └── GenGeneralUseCases.cs         # Casos de uso Gen General
├── Controllers/                          # Controladores REST API
│   ├── HelloWorldController.cs           # Endpoints Messages
│   ├── InfoController.cs                 # Health check e info
│   └── GenGeneralController.cs           # Endpoints Gen General
├── Domain/                               # Capa de Dominio
│   ├── Entities/
│   │   ├── Message.cs                    # Entidad Message
│   │   └── GenGeneral.cs                 # Entidad Gen General
│   └── Repositories/
│       ├── IMessageRepository.cs         # Interface Messages
│       └── IGenGeneralRepository.cs      # Interface Gen General
├── Infrastructure/                       # Capa de Infraestructura
│   ├── Data/
│   │   └── ApplicationDbContext.cs       # DbContext Entity Framework
│   └── Repositories/
│       ├── InMemoryMessageRepository.cs  # Repositorio en memoria
│       └── PostgreSqlGenGeneralRepository.cs # Repositorio PostgreSQL
├── Middleware/
│   └── GlobalExceptionMiddleware.cs      # Manejo global de errores
├── Migrations/                           # Migraciones Entity Framework
│   ├── 20240929_InitialCreate.cs
│   └── ApplicationDbContextModelSnapshot.cs
├── nginx/
│   └── nginx.conf                        # Configuración Nginx
├── tests/                                # Tests (estructura preparada)
├── .env                                  # Variables de entorno
├── .gitignore
├── appsettings.json                      # Configuración aplicación
├── appsettings.Development.json          # Configuración desarrollo
├── docker-compose.yml                    # Docker Compose producción
├── docker-compose.dev.yml               # Docker Compose desarrollo
├── Dockerfile                            # Imagen Docker
├── HelloWorldApi.csproj                  # Archivo proyecto .NET
├── Program.cs                            # Punto de entrada aplicación
└── README.md                             # Este archivo
```

## 🗄️ Estructura de Base de Datos

### Tabla sisa.gen_general

```sql
CREATE TABLE sisa.gen_general (
    id SERIAL PRIMARY KEY,
    codigo VARCHAR(50),
    nombre VARCHAR(200),
    descripcion VARCHAR(500),
    valor VARCHAR(500),
    activo BOOLEAN DEFAULT true,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_modificacion TIMESTAMP
);
```

### Entidad Message (En memoria)

- Repositorio en memoria para demostraciones
- No requiere base de datos
- Útil para pruebas y desarrollo

## 🔧 Configuración

### Variables de Entorno (.env)

```bash
# PostgreSQL Database Configuration
DB_HOST=localhost
DB_PORT=5432
DB_NAME=sisa
DB_USER=postgres
DB_PASSWORD=yourpassword
CONNECTION_STRING=Host=localhost;Port=5432;Database=sisa;Username=postgres;Password=yourpassword;

# Environment
ASPNETCORE_ENVIRONMENT=Development

# URL de escucha
ASPNETCORE_URLS=http://+:5000

# Zona horaria
TZ=America/Santiago
```

### Configuración appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=sisa;Username=postgres;Password=yourpassword;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Docker Compose Profiles

- **Default**: Solo la API
- **nginx**: API + Nginx como reverse proxy

### Entity Framework Commands

```bash
# Instalar herramientas EF Core
dotnet tool install --global dotnet-ef

# Crear nueva migración
dotnet ef migrations add NombreMigracion

# Aplicar migraciones a la BD
dotnet ef database update

# Remover última migración
dotnet ef migrations remove
```

## 🔄 CI/CD

El proyecto está preparado para implementar CI/CD con:

- Docker multi-stage builds optimizados
- Health checks para monitoreo
- Configuración de producción lista
- Separación de entornos (dev/prod)

## 📊 Monitoreo

- **Health Check**: `/api/info/health`
- **Swagger Docs**: `/swagger` (solo en Development)
- **API Info**: Endpoint raíz `/`

## 🏛️ Principios de Arquitectura Hexagonal

### Capa de Dominio (Domain)
- **Entities**: `Message.cs`, `GenGeneral.cs`
- **Repositories**: Interfaces que definen contratos (`IMessageRepository`, `IGenGeneralRepository`)
- **Sin dependencias externas**: Solo lógica de negocio pura

### Capa de Aplicación (Application)
- **Use Cases**: Orquestan la lógica de negocio (`MessageUseCases`, `GenGeneralUseCases`)
- **DTOs**: Objetos de transferencia de datos para comunicación externa
- **Depende solo del Dominio**

### Capa de Infraestructura (Infrastructure)
- **Repositorios**: Implementaciones concretas de las interfaces del dominio
- **Base de Datos**: Entity Framework DbContext y configuraciones
- **Servicios externos**: Acceso a APIs, archivos, etc.

### Capa de Interface (Controllers + Middleware)
- **Controllers**: Adaptadores HTTP REST
- **Middleware**: Interceptores de requests/responses
- **Configuración**: Program.cs con inyección de dependencias

## 🤝 Contribución

Este es un proyecto POC. Para contribuir:

1. Fork del proyecto
2. Crear branch para features
3. Implementar tests
4. Seguir principios de Clean Architecture
5. Crear Pull Request

## � TODO / Mejoras Futuras

- [ ] Tests unitarios e integración
- [ ] Autenticación JWT
- [ ] Logging estructurado (Serilog)
- [ ] Caché con Redis
- [ ] Rate limiting
- [ ] API versioning
- [ ] OpenAPI schemas mejorados
- [ ] Métricas y observabilidad

## �📄 Licencia

Este proyecto es una POC para fines educativos y de demostración.

## 📞 Soporte

Para consultas sobre este POC, contacta al equipo de desarrollo.

---

**Nota**: Este es un proyecto de demostración (POC) que implementa las mejores prácticas de arquitectura hexagonal con ASP.NET Core 8 y PostgreSQL, mostrando tanto repositorios en memoria como persistencia real en base de datos.