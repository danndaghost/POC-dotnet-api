namespace HelloWorldApi.Application.DTOs;

public record MessageDto(
    Guid Id,
    string Content,
    DateTime CreatedAt,
    string Author
);

public record CreateMessageDto(
    string Content,
    string Author
);

public record ApiInfoDto(
    string Name,
    string Version,
    string Description,
    string Architecture,
    string[] Layers,
    string Technology,
    DateTime Timestamp,
    string Environment
);