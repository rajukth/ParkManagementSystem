namespace ParkManagementSystem.Web.Areas.Admin.ViewModels;

public class UsersVm
{
    public int  UserId { get; set; }
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
    public string Email { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public string? ProfileUrl {get;set;}
    public string AccountStatus { get; set; }
    
}