using Microsoft.EntityFrameworkCore;
using ParkManagementSystem.Core;
using ParkManagementSystem.Core.Entities.ApplicationUser;

namespace ParkManagementSystem.Infrastructure.DataContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.RegisterCoreEntities();

    }
}