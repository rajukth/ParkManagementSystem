namespace ParkManagementSystem.ApplicationUser.Models;

public class CurrentUser
{
    public bool IsAuthenticated { get; set; } = false;
    public int? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? SessionToken { get; set; }
    public DateTime? LoginTime { get; set; }
    public string? IpAddress { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}