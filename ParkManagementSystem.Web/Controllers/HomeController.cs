using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ParkManagementSystem.Web.Attributes;
using ParkManagementSystem.Web.Models;

namespace ParkManagementSystem.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult NotFound()
    {
        return View();
    }

    public IActionResult Testimonial()
    {
        return View();
    }

    public IActionResult Team()
    {
        return View();
    }

    public IActionResult Package()
    {
        return View();
    }

    public IActionResult Attraction()
    {
        return View();
    }

    public IActionResult Gallery()
    {
        return View();
    }

    public IActionResult Feature()
    {
        return View();
    }

    public IActionResult Blog()
    {
        return View();
    }

    public IActionResult Service()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }
}