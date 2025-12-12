using System.ComponentModel.DataAnnotations;
using ParkManagementSystem.Web.Attributes;

namespace ParkManagementSystem.Web.Areas.Admin.ViewModels;

public class OrgDetailVm
{
    [Required(ErrorMessage = "Organization's name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Organization's address is required")]
    public string Address { get; set; }
    [Required(ErrorMessage = "Organization's contact no.  is required")]
    [DataType(DataType.PhoneNumber)]
    [Phone]
    public string ContactNumber { get; set; }
    [Required(ErrorMessage = "Organization's email is required. For admin user.")]
    [EmailAddress(ErrorMessage = "Organization's email is not valid.")]
    public string Email { get; set; }
    public string? LogoUrl { get; set; }
    [Required(ErrorMessage = "Organization's contact person name is required")]
    public string ContactPerson { get; set; }

    public string? PanNumber { get; set; }
    public string? RegistrationNumber { get; set; }
    [MaxFileSize(200 * 1024, ErrorMessage = "Logo size must not exceed 200KB")]
    [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Only JPG, JPEG, PNG files are allowed.")]
    public IFormFile? Logo { get; set; }
    public string? Slogan { get; set; }
}