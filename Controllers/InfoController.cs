using HelloWorldApi.Application.DTOs;
using HelloWorldApi.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class InfoController : ControllerBase
{
    private readonly MessageUseCases _messageUseCases;

    public InfoController(MessageUseCases messageUseCases)
    {
        _messageUseCases = messageUseCases;
    }

    /// <summary>
    /// Get API information and health status
    /// </summary>
    /// <returns>API information</returns>
    [HttpGet]
    public ActionResult<ApiInfoDto> GetApiInfo()
    {
        var apiInfo = _messageUseCases.GetApiInfo();
        return Ok(apiInfo);
    }

    /// <summary>
    /// Health check endpoint
    /// </summary>
    /// <returns>Health status</returns>
    [HttpGet("health")]
    public ActionResult<object> GetHealth()
    {
        var health = new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Version = "1.0.0",
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"
        };

        return Ok(health);
    }
}