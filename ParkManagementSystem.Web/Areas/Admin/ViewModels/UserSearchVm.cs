namespace ParkManagementSystem.Web.Areas.Admin.ViewModels;

public class UserSearchVm
{
    public string SearchText { get; set; }
    public string Status { get; set; }
    public List<UsersVm> UserList { get; set; }
    
}