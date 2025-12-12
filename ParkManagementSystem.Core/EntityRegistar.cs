using Microsoft.EntityFrameworkCore;
using ParkManagementSystem.Core.Entities.ApplicationUser;
using ParkManagementSystem.Core.Entities.Organization;

namespace ParkManagementSystem.Core;

public static class EntityRegistrar
{
    public static void RegisterCoreEntities(this ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("Users");
        builder.Entity<Role>().ToTable("Roles");
        builder.Entity<UserRole>().ToTable("UserRoles");
        builder.Entity<Organization>().ToTable("Organizations");
        
        
        
        
        // Composite key for UserRole
        builder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        builder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
    }
}