using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkManagementSystem.Core.Helper;
using ParkManagementSystem.Infrastructure.DataContext;
using ParkManagementSystem.Infrastructure.DataContext.Interface;
using ParkManagementSystem.Infrastructure.Helper;
using ParkManagementSystem.Infrastructure.Providers;
using ParkManagementSystem.Infrastructure.Providers.IProviders;

namespace ParkManagementSystem.Infrastructure;

public static class DiConfig
{
    public static IServiceCollection BaseConfig(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                services.BuildServiceProvider()
                    .GetRequiredService<IConfiguration>()
                    .GetConnectionString("DefaultConnection")));
        
        
        services.AddScoped<IConnectionStringProviders, ConnectionStringProviders>();
        services.AddSingleton<UrlHelper>();
        services.AddScoped<IUow, Uow>();
        return services;
    }
}