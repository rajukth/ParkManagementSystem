using ParkManagementSystem.ApplicationUser.Services.Interfaces;
using ParkManagementSystem.Core.Entities;
using ParkManagementSystem.Core.Entities.ApplicationUser;
using ParkManagementSystem.Infrastructure.DataContext.Interface;

namespace ParkManagementSystem.ApplicationUser.Services;

public class UserService:IUserService
{
    private readonly IUow _uow;

    public UserService(IUow uow)
    {
        _uow = uow;
    }

    public async Task AddAsync(User user)
    {
        user.IsLocked = false;
        await _uow.CreateAsync(user);
        await _uow.SaveChangesAsync();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(User user)
    {
        throw new NotImplementedException();
    }
}