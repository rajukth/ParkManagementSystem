namespace ParkManagementSystem.Core.Entities.ApplicationUser;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string AccountStatus { get; set; } = Core.Constants.AccStatus.Pending;
    public string RecStatus { get; set; } = Core.Constants.RecStatus.Active;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // --- Login tracking ---
    public int FailedLoginAttempts { get; set; } = 0;
    public DateTime? LastLoginAt { get; set; }       
    public string? LastLoginIp { get; set; }               
    public bool IsLocked { get; set; } = false;            

    // Optional: store session token for single-device login
    public string? CurrentSessionToken { get; set; }        
    public DateTime? SessionExpiresAt { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? PasswordResetTokenExpiry { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; }
    public string? ProfileUrl { get; set; }
}