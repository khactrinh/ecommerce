using Ecommerce.Consumer.Application.Interfaces;
using Ecommerce.Consumer.Application.Models;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Consumer.Infrastructure.Persistence;

public class InboxRepository : IInboxRepository
{
    private readonly ConsumerDbContext _db;

    public InboxRepository(ConsumerDbContext db)
    {
        _db = db;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _db.Set<InboxMessageEntity>()
            .AnyAsync(x => x.Id == id);
    }

    public async Task AddAsync(InboxMessage message)
    {
        _db.Add(new InboxMessageEntity
        {
            Id = message.Id,
            Type = message.Type,
            Content = message.Content,
            OccurredOn = message.OccurredOn
        });

        await _db.SaveChangesAsync();
    }

    public async Task MarkProcessedAsync(Guid id)
    {
        var msg = await _db.Set<InboxMessageEntity>()
            .FirstAsync(x => x.Id == id);

        msg.ProcessedOn = DateTime.UtcNow;

        await _db.SaveChangesAsync();
    }

    public Task IncrementRetryAsync(Guid id, string error)
    {
        throw new NotImplementedException();
    }
}