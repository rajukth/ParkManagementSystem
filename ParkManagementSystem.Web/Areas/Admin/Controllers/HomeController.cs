using Microsoft.AspNetCore.Mvc;
using ParkManagementSystem.Core.Constants;
using ParkManagementSystem.Web.Attributes;

namespace ParkManagementSystem.Web.Areas.Admin.Controllers;
[Area("Admin")]
[AppAuthorize($"{RoleConstant.SysAdmin},{RoleConstant.Admin}")]
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}