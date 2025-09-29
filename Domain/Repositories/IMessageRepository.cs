using HelloWorldApi.Domain.Entities;

namespace HelloWorldApi.Domain.Repositories;

public interface IMessageRepository
{
    Task<Message?> GetByIdAsync(Guid id);
    Task<IEnumerable<Message>> GetAllAsync();
    Task<Message> AddAsync(Message message);
    Task UpdateAsync(Message message);
    Task DeleteAsync(Guid id);
}