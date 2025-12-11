using System.Security.Claims;

namespace ParkManagementSystem.Web.Middleware;

public class UserRoleMiddleware
{
    private readonly RequestDelegate _next;

    public UserRoleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var roles = new List<string>();

        if (context.User.Identity?.IsAuthenticated == true)
        {
            roles = context.User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
        }

        context.Items["UserRoles"] = roles;

        await _next(context);
    }
}