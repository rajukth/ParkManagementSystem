using ParkManagementSystem.Core.Entities;
using ParkManagementSystem.Core.Entities.ApplicationUser;

namespace ParkManagementSystem.ApplicationUser.Services.Interfaces;

public interface IUserService
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}