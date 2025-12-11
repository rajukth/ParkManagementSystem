using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ParkManagementSystem.Web.Attributes;

public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;

    public AuthorizeAttribute(string role)
    {
        _role = role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userRoles = context.HttpContext.Items["UserRoles"] as List<string>;

        if (userRoles == null || !userRoles.Contains(_role))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}