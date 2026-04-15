using Ecommerce.Application.Orders.Sagas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations;

public class OrderSagaStateConfiguration : IEntityTypeConfiguration<OrderSagaState>
{
    public void Configure(EntityTypeBuilder<OrderSagaState> builder)
    {
        // =========================
        // PRIMARY KEY
        // =========================
        builder.HasKey(x => x.OrderId);

        // =========================
        // PROPERTIES
        // =========================
        builder.Property(x => x.Status)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.PaymentCompleted)
            .IsRequired();

        builder.Property(x => x.InventoryReserved)
            .IsRequired();

        // =========================
        // INDEX (optional nhưng nên có)
        // =========================
        builder.HasIndex(x => x.Status);
    }
}