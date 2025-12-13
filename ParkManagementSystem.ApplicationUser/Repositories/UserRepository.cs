using Microsoft.EntityFrameworkCore;
using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.Core.Constants;
using ParkManagementSystem.Core.Entities;
using ParkManagementSystem.Core.Entities.ApplicationUser;
using ParkManagementSystem.Infrastructure.DataContext;
using ParkManagementSystem.Infrastructure.Generics;

namespace ParkManagementSystem.ApplicationUser.Repositories;

public class UserRepository:GenericRepository<User>,IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> UserExists(string email)
    {
        return await CheckIfExistAsync(x => x.Email == email);
    }

    public async Task<List<User>> GetActiveUsers(bool includeSysAdmin)
    {
        var query = GetBaseQueryable()
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Where(u => u.RecStatus == RecStatus.Active);

        if (!includeSysAdmin)
        {
            query = query.Where(u => u.UserRoles.All(ur => ur.Role.RoleName != RoleConstant.SysAdmin));
        }

        return await query.ToListAsync();
    }
}