using ParkManagementSystem.Core.Entities;
using ParkManagementSystem.Core.Entities.ApplicationUser;

namespace ParkManagementSystem.ApplicationUser.Managers.Interfaces;

public interface IUserLoginManager
{
    Task<User> LoginAsync(string email, string password, string ipAddress);
    Task<string> GeneratePasswordResetTokenAsync(string email);
    Task ResetPasswordAsync(string email, string token, string newPassword);

}