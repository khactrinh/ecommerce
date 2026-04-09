using Ecommerce.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        // builder.ToTable("user_roles");
        //
        // builder.HasKey(x => new { x.UserId, x.RoleId });
        //
        // builder.Property(x => x.UserId)
        //     .HasColumnName("user_id");
        //
        // builder.Property(x => x.RoleId)
        //     .HasColumnName("role_id");
        //
        // builder.HasOne(x => x.Role)
        //     .WithMany()
        //     .HasForeignKey(x => x.RoleId);
        //
        // // 🔥 IMPORTANT: tránh EF tạo UserId1
        // builder.HasOne<User>()
        //     .WithMany(x => x.UserRoles)
        //     .HasForeignKey(x => x.UserId);
        
        builder.HasKey(x => new { x.UserId, x.RoleId });

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);

        
    }
}