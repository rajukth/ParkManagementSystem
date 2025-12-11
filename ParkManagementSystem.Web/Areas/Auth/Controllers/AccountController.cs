using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ParkManagementSystem.ApplicationUser.Managers.Interfaces;
using ParkManagementSystem.Web.Areas.Auth.Models;

namespace ParkManagementSystem.Web.Areas.Auth.Controllers;
[Area("Auth")]
public class AccountController : Controller
{
    private readonly IUserLoginManager _loginManager;
    private readonly IRoleManager _roleManager;

    public AccountController(IUserLoginManager loginManager, IRoleManager roleManager)
    {
        _loginManager = loginManager;
        _roleManager = roleManager;
    }

    [HttpGet("login")]
    public IActionResult Login(string? returnUrl)
    {
        var model = new LoginModel();
        model.ReturnUrl = returnUrl;
        return View(model);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel vm)
    {
        if (string.IsNullOrEmpty(vm.Email) || string.IsNullOrEmpty(vm.Password))
        {
            ModelState.AddModelError("", "Email and Password are required.");
            return View();
        }

        try
        {
            var user = await _loginManager.LoginAsync(
                vm.Email, 
                vm.Password,
                HttpContext.Connection.RemoteIpAddress?.ToString() ?? ""
            );

            var userRoles = await _roleManager.GetUserRoles(user.Id);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, "AppCookie");
            await HttpContext.SignInAsync("AppCookie", new ClaimsPrincipal(identity));

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserSessionToken", user.CurrentSessionToken ?? "");

            return vm.ReturnUrl!=null ? Redirect(vm.ReturnUrl) : RedirectToAction("Index", "Home",new {area=""});
        }
        catch
        {
            ModelState.AddModelError("", "Invalid credentials or account locked.");
            return View();
        }
    }


    // ---------- Logout ----------
    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    // ---------- Password Reset Request ----------
    [HttpGet("forgot-password")]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            ModelState.AddModelError("", "Email is required.");
            return View();
        }

        try
        {
            var token = await _loginManager.GeneratePasswordResetTokenAsync(email);

            // TODO: Send email with token
            TempData["Message"] = $"Password reset token generated: {token}";
            return RedirectToAction("Login");
        }
        catch
        {
            ModelState.AddModelError("", "User not found.");
            return View();
        }
    }

    // ---------- Password Reset ----------
    [HttpGet("reset-password")]
    public IActionResult ResetPassword(string token, string email)
    {
        ViewBag.Token = token;
        ViewBag.Email = email;
        return View();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(string email, string token, string newPassword)
    {
        try
        {
            await _loginManager.ResetPasswordAsync(email, token, newPassword);
            TempData["Message"] = "Password reset successfully.";
            return RedirectToAction("Login");
        }
        catch
        {
            ModelState.AddModelError("", "Invalid token or expired.");
            ViewBag.Token = token;
            ViewBag.Email = email;
            return View();
        }
    }

    [HttpGet("access-denied")]
    public IActionResult AccessDenied()
    {
        return View();
    }
}