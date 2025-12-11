using ParkManagementSystem.Core.Entities.ApplicationUser;

namespace ParkManagementSystem.ApplicationUser.Services.Interfaces;

public interface IRoleService
{
    Task<Role> CreateRoleAsync(string roleName);
}