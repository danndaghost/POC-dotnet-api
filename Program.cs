using HelloWorldApi.Application.UseCases;
using HelloWorldApi.Domain.Repositories;
using HelloWorldApi.Infrastructure.Repositories;
using HelloWorldApi.Infrastructure.Data;
using HelloWorldApi.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() 
    { 
        Title = "HelloWorld API", 
        Version = "v1",
        Description = "POC ASP.NET Core 8 with Hexagonal Architecture"
    });
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Database Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection - Hexagonal Architecture
// Repositories
builder.Services.AddSingleton<IMessageRepository, InMemoryMessageRepository>();
builder.Services.AddScoped<IGenGeneralRepository, PostgreSqlGenGeneralRepository>();

// Use Cases
builder.Services.AddScoped<MessageUseCases>();
builder.Services.AddScoped<GenGeneralUseCases>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HelloWorld API v1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz
    });
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Root endpoint
app.MapGet("/", () => new 
{ 
    Message = "HelloWorld API is running!", 
    Documentation = "/swagger",
    Health = "/api/info/health",
    Architecture = "Hexagonal Architecture",
    Timestamp = DateTime.UtcNow
});

app.Run();