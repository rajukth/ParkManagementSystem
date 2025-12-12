namespace ParkManagementSystem.Core.Entities.Organization;

public class Organization:BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }
    public string? Slogan { get; set; }
    public string Email { get; set; }
    public string? LogoUrl { get; set; }
    public string ContactPerson { get; set; }

    public string? PanNumber { get; set; }
    public string? RegistrationNumber { get; set; }
}

