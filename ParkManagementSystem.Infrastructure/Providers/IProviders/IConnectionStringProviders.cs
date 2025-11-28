namespace ParkManagementSystem.Infrastructure.Providers.IProviders
{
    public interface IConnectionStringProviders
    {
        string GetConnectionString();
        void UpdateAppSettings(string connectionString);
    }
}
