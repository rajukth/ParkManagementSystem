using ParkManagementSystem.Core.Entities;
using ParkManagementSystem.Core.Entities.ApplicationUser;
using ParkManagementSystem.Infrastructure.Generics.Interface;

namespace ParkManagementSystem.ApplicationUser.Repositories.Interfaces;

public interface IUserRepository:IGenericRepository<User>
{
    Task<bool> UserExists(string username);
    Task<List<User>> GetActiveUsers(bool includeSysAdmin=false);
}