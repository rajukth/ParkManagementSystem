namespace ParkManagementSystem.ApplicationUser.Managers.Interfaces;

public interface IRoleManager
{
    Task AssignRoleAsync(int userId, int roleId);
    Task<List<string>> GetUserRoles(int userId);
}