using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.Core.Entities.ApplicationUser;
using ParkManagementSystem.Infrastructure.DataContext;
using ParkManagementSystem.Infrastructure.Generics;

namespace ParkManagementSystem.ApplicationUser.Repositories;

public class UserRoleRepository:GenericRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}