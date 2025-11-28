using Microsoft.Extensions.Configuration;

namespace ParkManagementSystem.Core.Helper;

public class UrlHelper
{
    private readonly IConfiguration _configuration;

    public UrlHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateResetPasswordCallbackUrl(string token, string email)
    {
        var baseUrl = _configuration["AppSettings:BaseUrl"];
        return $"{baseUrl}/Account/ResetForgetPassword?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(email)}";
    }
}