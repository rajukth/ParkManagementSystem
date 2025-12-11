namespace ParkManagementSystem.Core.Entities.ApplicationUser;

public class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; } = string.Empty;

    public string RecStatus { get; set; } = Core.Constants.RecStatus.Active;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}