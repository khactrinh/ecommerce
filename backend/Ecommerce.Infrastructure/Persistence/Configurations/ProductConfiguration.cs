using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Price);

        builder.Property(x => x.Stock);

        builder.Property(x => x.ImageUrl);
        
        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(x => x.CategoryId)
            .HasDatabaseName("idx_products_category");

        builder.HasIndex(x => x.Price)
            .HasDatabaseName("idx_products_price");

        builder.HasIndex(x => x.Name)
            .HasDatabaseName("idx_products_name");
    }
}