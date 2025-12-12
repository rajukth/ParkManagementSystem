using Microsoft.AspNetCore.Mvc;
using ParkManagementSystem.Web.Attributes;

namespace ParkManagementSystem.Web.Areas.Admin.Controllers;
[Area("Admin")]
[AppAuthorize("Admin,SysAdmin")]
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}