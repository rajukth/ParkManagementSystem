using ParkManagementSystem.Core.Entities.ApplicationUser;

namespace ParkManagementSystem.ApplicationUser.Managers.Interfaces;

public interface IUserManager
{
    Task CreateUserWithRoles(User user, List<string> roles);
}