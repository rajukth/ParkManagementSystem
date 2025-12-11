using ParkManagementSystem.ApplicationUser.Managers.Interfaces;
using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.Core.Entities.ApplicationUser;
using ParkManagementSystem.Infrastructure.DataContext.Interface;

namespace ParkManagementSystem.ApplicationUser.Managers;

public class RoleManager:IRoleManager
{
    private readonly IUserRoleRepository _userRoleRepo;
    private readonly IUow _uow;

    public RoleManager(IUserRoleRepository userRoleRepo, IUow uow)
    {
        _userRoleRepo = userRoleRepo;
        _uow = uow;
    }

    public async Task AssignRoleAsync(int userId, int roleId)
    {
        var exists = await _userRoleRepo
            .CheckIfExistAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

        if (exists)
            return;

        var ur = new UserRole
        {
            UserId = userId,
            RoleId = roleId
        };

        await _uow.CreateAsync(ur);
        await _uow.SaveChangesAsync();
    }

    public async Task<List<string>> GetUserRoles(int userId)
    {
        var list = await _userRoleRepo
            .GetAllAsync(ur => ur.UserId == userId,x=>x.Role);

        return list.Select(x => x.Role.RoleName).ToList();
    }
}