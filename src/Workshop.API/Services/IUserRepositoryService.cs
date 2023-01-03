using Workshop.API.Models;

namespace Workshop.API.Services
{
    public interface IUserRepositoryService
    {
        Task<bool> ConfirmEmailAsync(WorkshopUser user, string token);
        Task<bool> CreateUserAsync(RegisterRequest credentials);
        Task<RefreshToken> CreateUserRefreshTokenAsync(string email);
        Task<bool> DeleteRefreshTokenAsync(string token);
        Task<string> GenerateEmailConfirmationTokenAsync(WorkshopUser user);
        Task<WorkshopUser> GetUserByEmailAsync(string email);
        Task<WorkshopUser> GetUserByIdAsync(string id);
        Task<RefreshToken?> IsRefreshTokenValidAsync(WorkshopUser user, string token);
        Task<bool> IsUserValidAsync(AuthenticationRequest credentials);
        Task<int> SaveChangesAsync();
        Task<bool> ValidateEmailConfirmAsync(WorkshopUser user);
    }
}