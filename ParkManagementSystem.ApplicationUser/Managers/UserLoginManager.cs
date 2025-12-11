using System.Security.Cryptography;
using System.Text;
using ParkManagementSystem.ApplicationUser.Managers.Interfaces;
using ParkManagementSystem.ApplicationUser.Repositories.Interfaces;
using ParkManagementSystem.Core.Entities;
using ParkManagementSystem.Core.Entities.ApplicationUser;
using ParkManagementSystem.Infrastructure.DataContext.Interface;
using ParkManagementSystem.Infrastructure.Exceptions;
using ParkManagementSystem.Infrastructure.Generics.Interface;

namespace ParkManagementSystem.ApplicationUser.Managers;

public class UserLoginManager:IUserLoginManager
{
    private readonly IUserRepository _userRepository;
    private readonly IUow _uow;

        private const int MaxFailedLoginAttempts = 5;
        private const int PasswordResetTokenExpiryMinutes = 30;

        public UserLoginManager(IUserRepository userRepository, IUow uow)
        {
            _userRepository = userRepository;
            _uow = uow;
        }

        // ---------------- LOGIN ----------------
        public async Task<User> LoginAsync(string email, string password, string ipAddress)
        {
            var user = await _userRepository.GetItemAsync(u => u.Email == email);
            if (user == null) throw new EntityNotFoundException("User not found");

            if (user.IsLocked)
                throw new InvalidOperationException("User account is locked due to multiple failed login attempts.");

            if (!VerifyPassword(password, user.PasswordHash))
            {
                user.FailedLoginAttempts++;
                if (user.FailedLoginAttempts >= MaxFailedLoginAttempts)
                    user.IsLocked = true;

                await _uow.SaveChangesAsync();
                throw new InvalidOperationException("Invalid credentials");
            }

            // Successful login
            user.FailedLoginAttempts = 0;
            user.LastLoginAt = DateTime.UtcNow;
            user.LastLoginIp = ipAddress;

            // Optional: generate session token
            user.CurrentSessionToken = GenerateSessionToken();
            user.SessionExpiresAt = DateTime.UtcNow.AddHours(8);

            await _uow.SaveChangesAsync();
            return user;
        }

        // ---------------- PASSWORD RESET ----------------
        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userRepository.GetItemAsync(u => u.Email == email);
            if (user == null) throw new EntityNotFoundException("User not found");

            var token = GenerateSecureToken();
            // Save token + expiry in user entity (or separate table)
            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiry = DateTime.UtcNow.AddMinutes(PasswordResetTokenExpiryMinutes);
            await _uow.SaveChangesAsync();

            return token; // send via email
        }

        public async Task ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userRepository.GetItemAsync(u => u.Email == email);
            if (user == null) throw new EntityNotFoundException("User not found");

            if (user.PasswordResetToken != token || user.PasswordResetTokenExpiry < DateTime.UtcNow)
                throw new InvalidOperationException("Invalid or expired password reset token");

            user.PasswordHash = HashPassword(newPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = null;

            await _uow.SaveChangesAsync();
        }

        // ---------------- HELPER METHODS ----------------
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static bool VerifyPassword(string password, string passwordHash)
        {
            return HashPassword(password) == passwordHash;
        }

        private static string GenerateSessionToken()
        {
            return Guid.NewGuid().ToString("N");
        }

        private static string GenerateSecureToken()
        {
            return Guid.NewGuid().ToString("N");
        }
}