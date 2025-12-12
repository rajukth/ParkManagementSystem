using ParkManagementSystem.ApplicationUser.Managers.Interfaces;
using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.ApplicationUser.Services.Interfaces;
using ParkManagementSystem.Core.Entities.ApplicationUser;

namespace ParkManagementSystem.ApplicationUser.Managers;

public class UserManager:IUserManager
{
    private readonly IRoleManager _roleManager;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserService _userService;

    public UserManager(IRoleManager roleManager, IRoleRepository roleRepository, IUserService userService)
    {
        _roleManager = roleManager;
        _roleRepository = roleRepository;
        _userService = userService;
    }

    public async Task CreateUserWithRoles(User user, List<string> roles)
    {
        try
        {
            await _userService.AddAsync(user);
            foreach (var role in roles)
            {
                var data = await _roleRepository.GetItemAsync(x => x.RoleName == role);
                await _roleManager.AssignRoleAsync(user.Id, data.Id);
            }
        }
        catch (Exception e)
        {
            throw;
        }
    }
}