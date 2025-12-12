using ParkManagementSystem.Core.Entities.Organization;

namespace ParkManagementSystem.ApplicationUser.Services.Interfaces;

public interface IOrganizationService
{
    Task CreateAsync(Organization organization);
    Task UploadLogoAsync(string? logoUrl);
}