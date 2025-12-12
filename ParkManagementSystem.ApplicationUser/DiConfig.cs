using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkManagementSystem.ApplicationUser.Managers;
using ParkManagementSystem.ApplicationUser.Managers.Interfaces;
using ParkManagementSystem.ApplicationUser.Provider;
using ParkManagementSystem.ApplicationUser.Provider.Interface;
using ParkManagementSystem.ApplicationUser.Repositories;
using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.ApplicationUser.Services;
using ParkManagementSystem.ApplicationUser.Services.Interfaces;
using ParkManagementSystem.Core.Helper;
using ParkManagementSystem.Infrastructure.DataContext;
using ParkManagementSystem.Infrastructure.DataContext.Interface;
using ParkManagementSystem.Infrastructure.Helper;
using ParkManagementSystem.Infrastructure.Providers;
using ParkManagementSystem.Infrastructure.Providers.IProviders;

namespace ParkManagementSystem.ApplicationUser;

public static class DiConfig
{
    public static IServiceCollection AppUserConfig(this IServiceCollection services)
    {
        /*manager*/
        services.AddScoped<IUserLoginManager,UserLoginManager>()
            .AddScoped<IRoleManager,RoleManager>()
            ;
        
        /*Repositories*/
        services.AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserRoleRepository, UserRoleRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            ;
        
        /*Services*/
        services.AddScoped<IUserService,UserService>()
            .AddScoped<IRoleService,RoleService>()
            ;
        /*Provider*/
        services.AddScoped<ICurrentUserProvider,CurrentUserProvider>()
            ;
        
        
        return services;
    }
}