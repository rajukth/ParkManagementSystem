using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ParkManagementSystem.ApplicationUser.Models;
using ParkManagementSystem.ApplicationUser.Provider.Interface;
using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.Infrastructure.DataContext;

namespace ParkManagementSystem.ApplicationUser.Provider;

public class CurrentUserProvider:ICurrentUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;
    
    private CurrentUser? _cachedUser;

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public async Task<CurrentUser> GetCurrentUserAsync()
    {
        if (_cachedUser != null)
            return _cachedUser;

        var context = _httpContextAccessor.HttpContext;

        if (context == null || context.User?.Identity?.IsAuthenticated != true)
        {
            _cachedUser = new CurrentUser { IsAuthenticated = false };
            return _cachedUser;
        }

        // Get user id from claims
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out int userId))
        {
            _cachedUser = new CurrentUser { IsAuthenticated = false };
            return _cachedUser;
        }

        // Fetch user with roles from DB
        var user = await _userRepository.GetBaseQueryable()
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            _cachedUser = new CurrentUser { IsAuthenticated = false };
            return _cachedUser;
        }

        // Map DB user to CurrentUser
        _cachedUser = new CurrentUser
        {
            IsAuthenticated = true,
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            SessionToken = user.CurrentSessionToken,
            LoginTime = user.LastLoginAt,
            IpAddress = user.LastLoginIp,
            Roles = user.UserRoles?.Select(ur => ur.Role.RoleName).ToList() ?? new List<string>()
        };

        return _cachedUser;
    }
    
    public CurrentUser GetCurrentUser()
    {
        if (_cachedUser != null)
            return _cachedUser;

        var context = _httpContextAccessor.HttpContext;

        if (context == null || context.User?.Identity?.IsAuthenticated != true)
        {
            _cachedUser = new CurrentUser { IsAuthenticated = false };
            return _cachedUser;
        }

        // Get user id from claims
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out int userId))
        {
            _cachedUser = new CurrentUser { IsAuthenticated = false };
            return _cachedUser;
        }

        // Fetch user with roles from DB
        var user = _userRepository.GetBaseQueryable()
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefault(u => u.Id == userId);

        if (user == null)
        {
            _cachedUser = new CurrentUser { IsAuthenticated = false };
            return _cachedUser;
        }

        // Map DB user to CurrentUser
        _cachedUser = new CurrentUser
        {
            IsAuthenticated = true,
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            SessionToken = user.CurrentSessionToken,
            LoginTime = user.LastLoginAt,
            IpAddress = user.LastLoginIp,
            Roles = user.UserRoles?.Select(ur => ur.Role.RoleName).ToList() ?? new List<string>()
        };

        return _cachedUser;
    }
}