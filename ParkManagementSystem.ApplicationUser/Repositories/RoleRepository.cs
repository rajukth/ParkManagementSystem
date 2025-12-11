using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.Core.Entities.ApplicationUser;
using ParkManagementSystem.Infrastructure.DataContext;
using ParkManagementSystem.Infrastructure.Generics;

namespace ParkManagementSystem.ApplicationUser.Repositories;

public class RoleRepository:GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}