using Microsoft.AspNetCore.Mvc;
using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.Core.Constants;
using ParkManagementSystem.Core.Helper;
using ParkManagementSystem.Web.Areas.Admin.ViewModels;
using ParkManagementSystem.Web.Attributes;

namespace ParkManagementSystem.Web.Areas.Admin.Controllers;
[Area("Admin")]
[AppAuthorize($"{RoleConstant.SysAdmin},{RoleConstant.Admin}")]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var vm = new UserSearchVm();
        var userList=await _userRepository.GetActiveUsers();
        vm.UserList = userList.Select(u => new UsersVm
        {
            UserId = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            LastLoginAt = u.LastLoginAt,
            ProfileUrl = u.ProfileUrl,
            AccountStatus = ConstantHelper.GetName<string>(u.AccountStatus),

            Roles = u.UserRoles?
                .Where(ur => ur.Role != null)
                .Select(ur => ur.Role.RoleName)
                .ToList() ?? new List<string>()

        }).ToList();
        return View(vm);
    }
}