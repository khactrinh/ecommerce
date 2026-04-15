using Ecommerce.Consumer.Application.Models;

namespace Ecommerce.Consumer.Application.Interfaces;


public interface IInboxRepository
{
    Task<bool> ExistsAsync(Guid id);
    Task AddAsync(InboxMessage message);
    Task MarkProcessedAsync(Guid id);
    Task IncrementRetryAsync(Guid id, string error);
}