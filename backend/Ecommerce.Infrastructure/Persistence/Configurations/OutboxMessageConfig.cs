using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations;

public class OutboxMessageConfig : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.Content).IsRequired();
        
        // 🔥 index cho polling nhanh
        builder.HasIndex(x => x.ProcessedOn)
            .HasDatabaseName("idx_outbox_unprocessed");

        // optional composite
        builder.HasIndex(x => new { x.ProcessedOn, x.OccurredOn });
    }
}