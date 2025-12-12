using ParkManagementSystem.Core.Entities.Organization;
using ParkManagementSystem.Infrastructure.Generics.Interface;

namespace ParkManagementSystem.ApplicationUser.Repositories.Interfaces;

public interface IOrganizationRepository:IGenericRepository<Organization>
{
    Task<Organization?> GetOrganizationDetail();
}