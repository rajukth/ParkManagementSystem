using ParkManagementSystem.ApplicationUser.Provider.Interface;
using ParkManagementSystem.Core.Constants;

namespace ParkManagementSystem.Web.Helper;
public interface ILayoutHelper
{
    Task<string> GetLayoutAsync();
}

public class LayoutHelper : ILayoutHelper
{
    private readonly ICurrentUserProvider _currentUserProvider;

    public LayoutHelper(ICurrentUserProvider currentUserProvider)
    {
        _currentUserProvider = currentUserProvider;
    }

    public async Task<string> GetLayoutAsync()
    {
        var currentUser = await _currentUserProvider.GetCurrentUserAsync();

        if (!currentUser.IsAuthenticated)
            return "_Layout"; // default layout for guest

        // You can prioritize roles if user has multiple
        if (currentUser.Roles.Contains(RoleConstant.SysAdmin) || currentUser.Roles.Contains(RoleConstant.Admin))
            return "_AdminLayout";

        if (currentUser.Roles.Contains(RoleConstant.Manager))
            return "_ManagerLayout";

        // Default layout for regular authenticated users
        return "_Layout";
    }
}