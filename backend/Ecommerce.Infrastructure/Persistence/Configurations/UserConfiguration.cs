using Ecommerce.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // builder.ToTable("users");
        //
        // builder.HasKey(x => x.Id);
        //
        // builder.Property(x => x.Email)
        //     .IsRequired()
        //     .HasMaxLength(200);
        //
        // // để tránh:
        // // Admin@gmail.com != admin@gmail.com
        // builder.Property(x => x.Email)
        //     .IsRequired()
        //     .HasMaxLength(200)
        //     .HasConversion(
        //         v => v.ToLower(),
        //         v => v
        //     );
        //
        // builder.Property(x => x.PasswordHash)
        //     .IsRequired()
        //     .HasMaxLength(500);
        //
        // builder.Ignore(x => x.Roles);
        //
        // // 🔥 Map backing field đúng cách
        // builder.Navigation(x => x.UserRoles)
        //     .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200)
            .HasConversion(
                v => v.ToLower(),
                v => v
            );

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Ignore(x => x.Roles);

        builder.HasMany(x => x.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // 🔥 dùng field access
        builder.Navigation(x => x.UserRoles)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        
    }
}