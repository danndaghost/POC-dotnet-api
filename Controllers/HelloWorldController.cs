using HelloWorldApi.Application.DTOs;
using HelloWorldApi.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class HelloWorldController : ControllerBase
{
    private readonly MessageUseCases _messageUseCases;

    public HelloWorldController(MessageUseCases messageUseCases)
    {
        _messageUseCases = messageUseCases;
    }

    /// <summary>
    /// Get a Hello World message
    /// </summary>
    /// <returns>A Hello World message</returns>
    [HttpGet]
    public async Task<ActionResult<MessageDto>> GetHelloWorld()
    {
        var message = await _messageUseCases.GetHelloWorldAsync();
        return Ok(message);
    }

    /// <summary>
    /// Get all messages
    /// </summary>
    /// <returns>List of all messages</returns>
    [HttpGet("messages")]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetAllMessages()
    {
        var messages = await _messageUseCases.GetAllMessagesAsync();
        return Ok(messages);
    }

    /// <summary>
    /// Create a new message
    /// </summary>
    /// <param name="createMessageDto">Message data</param>
    /// <returns>Created message</returns>
    [HttpPost("messages")]
    public async Task<ActionResult<MessageDto>> CreateMessage([FromBody] CreateMessageDto createMessageDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var message = await _messageUseCases.CreateMessageAsync(createMessageDto);
        return CreatedAtAction(nameof(GetHelloWorld), new { id = message.Id }, message);
    }
}