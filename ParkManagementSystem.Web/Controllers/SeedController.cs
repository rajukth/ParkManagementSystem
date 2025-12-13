using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkManagementSystem.ApplicationUser.Managers;
using ParkManagementSystem.ApplicationUser.Managers.Interfaces;
using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.ApplicationUser.Services.Interfaces;
using ParkManagementSystem.Core.Constants;
using ParkManagementSystem.Core.Entities.ApplicationUser;

namespace ParkManagementSystem.Web.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
public class SeedController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IRoleManager _roleManager;
    private readonly IConfiguration _config;

    public SeedController(
        IConfiguration config,
        IUserRepository userRepository, IUserService userService, IRoleService roleService, IRoleManager roleManager 
        )
    {
        _userRepository = userRepository;
        _userService = userService;
        _roleService = roleService;
        _roleManager = roleManager;
        _config = config;
    }

    // GET
    public async Task<IActionResult> Index(string key)
    {
        var seedKey = _config["SeedSettings:SeedKey"];

        if (key != seedKey)
            return Unauthorized("Invalid seed key");
        
        await SeedRole();
        await SeedUser();
        return Content("Data Seeded Successfully");
    }

    private async Task SeedUser()
    {
        var sysAdminRole = await _roleService.CreateRoleAsync(RoleConstant.SysAdmin);
        var existingAdmin = await _userRepository.UserExists("admin@gmail.com");
        if (!existingAdmin)
        {
            var adminUser = new User
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                PasswordHash = UserLoginManager.HashPassword("Admin@123"),
                AccountStatus = Core.Constants.AccStatus.Approved,
                RecStatus = Core.Constants.RecStatus.Active
            };
            await _userService.AddAsync(adminUser);
            await _roleManager.AssignRoleAsync(adminUser.Id, sysAdminRole.Id);
        }
    }

    private async Task SeedRole()
    {
        var admin = await _roleService.CreateRoleAsync(RoleConstant.Admin);
        var manager = await _roleService.CreateRoleAsync(RoleConstant.Manager);
        var accountant = await _roleService.CreateRoleAsync(RoleConstant.Accountant);
        var user= await _roleService.CreateRoleAsync(RoleConstant.User);
        var guest = await _roleService.CreateRoleAsync(RoleConstant.Guest);
        var storekeeper = await _roleService.CreateRoleAsync(RoleConstant.StoreKeeper);
    }

}