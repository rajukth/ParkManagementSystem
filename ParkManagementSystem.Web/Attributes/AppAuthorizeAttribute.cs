using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ParkManagementSystem.Web.Attributes;

public class AppAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly List<string> _roles;

    public AppAuthorizeAttribute(string roles)
    {
        _roles = roles.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        
        if (user?.Identity?.IsAuthenticated != true)
        {
            var returnUrl = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;

            context.Result = new RedirectToActionResult(
                "Login","Account", new { area = "auth",returnUrl });
            return;
        }
        
        var userRoles = context.HttpContext.Items["UserRoles"] as List<string>;

        if (userRoles == null || !_roles.Any(r => userRoles.Contains(r)))
        {
            context.Result = new RedirectToActionResult(
                "AccessDenied",
                "Account",
                new { area = "auth" });
        }
    }
}