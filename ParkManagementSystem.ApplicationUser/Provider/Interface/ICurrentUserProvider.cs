using ParkManagementSystem.ApplicationUser.Models;

namespace ParkManagementSystem.ApplicationUser.Provider.Interface;

public interface ICurrentUserProvider
{
    Task<CurrentUser> GetCurrentUserAsync();
    CurrentUser GetCurrentUser();
}