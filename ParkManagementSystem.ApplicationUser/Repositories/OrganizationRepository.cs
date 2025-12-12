using Microsoft.EntityFrameworkCore;
using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.Core.Entities.Organization;
using ParkManagementSystem.Infrastructure.DataContext;
using ParkManagementSystem.Infrastructure.Generics;

namespace ParkManagementSystem.ApplicationUser.Repositories;

public class OrganizationRepository:GenericRepository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Organization?> GetOrganizationDetail()
    {
        return await GetBaseQueryable().FirstOrDefaultAsync();
    }
}