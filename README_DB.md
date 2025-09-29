# HelloWorld API - POC ASP.NET Core 8 con Arquitectura Hexagonal

Este es un proyecto de prueba de concepto (POC) que demuestra una API REST desarrollada en ASP.NET Core 8 utilizando arquitectura hexagonal y conectada a PostgreSQL.

## Características

- ✅ ASP.NET Core 8
- ✅ Arquitectura Hexagonal (Domain, Application, Infrastructure, Controllers)
- ✅ Entity Framework Core con PostgreSQL
- ✅ Swagger/OpenAPI para documentación
- ✅ Docker y Docker Compose
- ✅ Manejo global de excepciones
- ✅ CORS configurado
- ✅ Inyección de dependencias

## Estructura del Proyecto

```
HelloWorldApi/
├── Domain/
│   ├── Entities/
│   │   ├── Message.cs
│   │   └── GenGeneral.cs
│   └── Repositories/
│       ├── IMessageRepository.cs
│       └── IGenGeneralRepository.cs
├── Application/
│   ├── DTOs/
│   │   ├── MessageDto.cs
│   │   └── GenGeneralDto.cs
│   └── UseCases/
│       ├── MessageUseCases.cs
│       └── GenGeneralUseCases.cs
├── Infrastructure/
│   ├── Data/
│   │   └── ApplicationDbContext.cs
│   └── Repositories/
│       ├── InMemoryMessageRepository.cs
│       └── PostgreSqlGenGeneralRepository.cs
├── Controllers/
│   ├── HelloWorldController.cs
│   ├── InfoController.cs
│   └── GenGeneralController.cs
└── Middleware/
    └── GlobalExceptionMiddleware.cs
```

## Configuración de Base de Datos

### 1. Configurar PostgreSQL

Asegúrate de tener PostgreSQL ejecutándose y crea la base de datos `sisa`:

```sql
CREATE DATABASE sisa;
CREATE SCHEMA IF NOT EXISTS sisa;
```

### 2. Configurar Credenciales

Edita el archivo `.env` con tus credenciales de PostgreSQL:

```env
DB_HOST=localhost
DB_PORT=5432
DB_NAME=sisa
DB_USER=postgres
DB_PASSWORD=tu_password_aqui
CONNECTION_STRING=Host=localhost;Port=5432;Database=sisa;Username=postgres;Password=tu_password_aqui;
```

También actualiza `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=sisa;Username=postgres;Password=tu_password_aqui;"
  }
}
```

### 3. Ejecutar Migraciones

```bash
# Actualizar la base de datos con las migraciones
export PATH="$PATH:/home/daniel/.dotnet/tools"
dotnet ef database update
```

## Ejecución

### Desarrollo Local

```bash
# Instalar dependencias
dotnet restore

# Ejecutar la aplicación
dotnet run

# La API estará disponible en:
# - http://localhost:5000
# - https://localhost:5001
# - Swagger UI: http://localhost:5000/swagger
```

### Docker

```bash
# Construir y ejecutar con Docker Compose
docker-compose up --build

# La API estará disponible en:
# - http://localhost:8080
```

## Endpoints Disponibles

### API Información
- `GET /` - Información general de la API
- `GET /api/info/health` - Health check

### Messages (En Memoria)
- `GET /api/helloworld` - Obtener todos los mensajes
- `POST /api/helloworld/messages` - Crear nuevo mensaje
- `GET /api/helloworld/messages/{id}` - Obtener mensaje por ID

### Gen General (PostgreSQL - Tabla sisa.gen_general)
- `GET /api/gengeneral` - Obtener todos los registros
- `GET /api/gengeneral/activos` - Obtener solo registros activos
- `GET /api/gengeneral/{id}` - Obtener registro por ID
- `GET /api/gengeneral/codigo/{codigo}` - Obtener registro por código
- `POST /api/gengeneral` - Crear nuevo registro
- `PUT /api/gengeneral/{id}` - Actualizar registro
- `DELETE /api/gengeneral/{id}` - Eliminar registro
- `HEAD /api/gengeneral/{id}` - Verificar si existe registro

## Estructura de la Tabla gen_general

La tabla `sisa.gen_general` tiene la siguiente estructura esperada:

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

## Ejemplos de Uso

### Crear un registro en gen_general

```bash
curl -X POST http://localhost:5000/api/gengeneral \
  -H "Content-Type: application/json" \
  -d '{
    "codigo": "TEST_001",
    "nombre": "Registro de Prueba",
    "descripcion": "Este es un registro de prueba",
    "valor": "valor_ejemplo",
    "activo": true
  }'
```

### Obtener todos los registros activos

```bash
curl http://localhost:5000/api/gengeneral/activos
```

## Arquitectura Hexagonal

Este proyecto implementa los principios de arquitectura hexagonal:

- **Domain**: Entidades de negocio e interfaces de repositorios
- **Application**: Casos de uso y DTOs
- **Infrastructure**: Implementaciones de repositorios y acceso a datos
- **Controllers**: Adaptadores de entrada (HTTP/REST)

## Tecnologías Utilizadas

- ASP.NET Core 8.0
- Entity Framework Core 9.0
- Npgsql (PostgreSQL Provider)
- Swashbuckle.AspNetCore (Swagger)
- Docker & Docker Compose

## Próximos Pasos

Para una implementación completa, considera:

- [ ] Autenticación y autorización (JWT)
- [ ] Logging estructurado (Serilog)
- [ ] Caché (Redis)
- [ ] Pruebas unitarias e integración
- [ ] Monitoreo y métricas
- [ ] CI/CD Pipeline