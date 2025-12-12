using Microsoft.EntityFrameworkCore;
using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.ApplicationUser.Services.Interfaces;
using ParkManagementSystem.Core.Entities.Organization;
using ParkManagementSystem.Infrastructure.DataContext.Interface;

namespace ParkManagementSystem.ApplicationUser.Services;

public class OrganizationService:IOrganizationService
{
    private readonly IUow _uow;
    private readonly IOrganizationRepository _organizationRepository;    
    public OrganizationService(IUow uow, IOrganizationRepository organizationRepository)
    {
        _uow = uow;
        _organizationRepository = organizationRepository;
    }


    public async Task CreateAsync(Organization organization)
    {
        var isExist = await _organizationRepository.CheckIfExistAsync();
        if (!isExist)
        {
            await _uow.CreateAsync(organization);
            await _uow.SaveChangesAsync();
        }
    }

    public async Task UploadLogoAsync(string? logoUrl)
    {
        var org = await _organizationRepository.GetBaseQueryable().FirstOrDefaultAsync();
        if (org == null)
        {
            throw new ArgumentException($"Organization is not registered yet");
        }
        org.LogoUrl = logoUrl;
        await _uow.SaveChangesAsync();
    }
}