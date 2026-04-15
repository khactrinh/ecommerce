using Ecommerce.Consumer.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Consumer.Infrastructure.Persistence;

public class ConsumerDbContext : DbContext
{
    public ConsumerDbContext(DbContextOptions<ConsumerDbContext> options)
        : base(options) { }

    public DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConsumerDbContext).Assembly);
    }
}