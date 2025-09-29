using HelloWorldApi.Domain.Entities;
using HelloWorldApi.Domain.Repositories;

namespace HelloWorldApi.Infrastructure.Repositories;

public class InMemoryMessageRepository : IMessageRepository
{
    private readonly List<Message> _messages = new();

    public Task<Message?> GetByIdAsync(Guid id)
    {
        var message = _messages.FirstOrDefault(m => m.Id == id);
        return Task.FromResult(message);
    }

    public Task<IEnumerable<Message>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Message>>(_messages.ToList());
    }

    public Task<Message> AddAsync(Message message)
    {
        _messages.Add(message);
        return Task.FromResult(message);
    }

    public Task UpdateAsync(Message message)
    {
        var existingMessage = _messages.FirstOrDefault(m => m.Id == message.Id);
        if (existingMessage != null)
        {
            _messages.Remove(existingMessage);
            _messages.Add(message);
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var message = _messages.FirstOrDefault(m => m.Id == id);
        if (message != null)
        {
            _messages.Remove(message);
        }
        return Task.CompletedTask;
    }
}