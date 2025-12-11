using Microsoft.EntityFrameworkCore;
using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.Core.Entities;
using ParkManagementSystem.Core.Entities.ApplicationUser;
using ParkManagementSystem.Infrastructure.DataContext;
using ParkManagementSystem.Infrastructure.Generics;

namespace ParkManagementSystem.ApplicationUser.Repositories;

public class UserRepository:GenericRepository<User>,IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> UserExists(string email)
    {
        return await CheckIfExistAsync(x => x.Email == email);
    }
}