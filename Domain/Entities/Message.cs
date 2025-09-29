namespace HelloWorldApi.Domain.Entities;

public class Message
{
    public Guid Id { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Author { get; private set; }

    public Message(string content, string author)
    {
        Id = Guid.NewGuid();
        Content = content ?? throw new ArgumentNullException(nameof(content));
        Author = author ?? throw new ArgumentNullException(nameof(author));
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateContent(string newContent)
    {
        if (string.IsNullOrWhiteSpace(newContent))
            throw new ArgumentException("Content cannot be null or empty", nameof(newContent));
        
        Content = newContent;
    }
}