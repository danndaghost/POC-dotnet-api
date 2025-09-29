using HelloWorldApi.Application.DTOs;
using HelloWorldApi.Domain.Entities;
using HelloWorldApi.Domain.Repositories;

namespace HelloWorldApi.Application.UseCases;

public class MessageUseCases
{
    private readonly IMessageRepository _messageRepository;

    public MessageUseCases(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<MessageDto> GetHelloWorldAsync()
    {
        var message = new Message("Hello World from ASP.NET Core 8 with Hexagonal Architecture!", "API System");
        await _messageRepository.AddAsync(message);

        return new MessageDto(
            message.Id,
            message.Content,
            message.CreatedAt,
            message.Author
        );
    }

    public async Task<IEnumerable<MessageDto>> GetAllMessagesAsync()
    {
        var messages = await _messageRepository.GetAllAsync();
        return messages.Select(m => new MessageDto(m.Id, m.Content, m.CreatedAt, m.Author));
    }

    public async Task<MessageDto> CreateMessageAsync(CreateMessageDto createDto)
    {
        var message = new Message(createDto.Content, createDto.Author);
        await _messageRepository.AddAsync(message);

        return new MessageDto(
            message.Id,
            message.Content,
            message.CreatedAt,
            message.Author
        );
    }

    public ApiInfoDto GetApiInfo()
    {
        return new ApiInfoDto(
            Name: "HelloWorld API",
            Version: "1.0.0",
            Description: "POC ASP.NET Core 8 with Hexagonal Architecture",
            Architecture: "Hexagonal (Ports and Adapters)",
            Layers: new[] { "Domain", "Application", "Infrastructure", "Controllers" },
            Technology: ".NET 8",
            Timestamp: DateTime.UtcNow,
            Environment: Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"
        );
    }
}