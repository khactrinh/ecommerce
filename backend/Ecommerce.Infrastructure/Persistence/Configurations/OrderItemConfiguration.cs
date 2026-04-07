using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property<Guid>("OrderId");

        builder.Property(x => x.ProductName)
            .HasMaxLength(255);

        builder.Property(x => x.Price)
            .HasColumnType("decimal(18,2)");
    }
}