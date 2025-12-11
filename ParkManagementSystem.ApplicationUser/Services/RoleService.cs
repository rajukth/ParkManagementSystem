using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.ApplicationUser.Services.Interfaces;
using ParkManagementSystem.Core.Entities.ApplicationUser;
using ParkManagementSystem.Infrastructure.DataContext.Interface;

namespace ParkManagementSystem.ApplicationUser.Services;

public class RoleService:IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUow _uow;

    public RoleService(IRoleRepository roleRepository, IUow uow)
    {
        _roleRepository = roleRepository;
        _uow = uow;
    }

    public async Task<Role> CreateRoleAsync(string roleName)
    {
        var existing = await _roleRepository.GetItemAsync(r => r.RoleName == roleName);
        if (existing != null)
            return existing;
        var role = new Role { RoleName = roleName };
        await _uow.CreateAsync(role);
        await _uow.SaveChangesAsync();
        return role;
    }
}