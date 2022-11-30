using Workshop.API.Models;

namespace Workshop.API.Services
{
    public interface IUserRepositoryService
    {
        Task<bool> CreateUserAsync(RegisterRequest credentials);
        Task<RefreshToken> CreateUserRefreshTokenAsync(string email);
        Task<bool> DeleteTokenAsync(string token);
        Task<WorkshopUser> GetUserByEmailAsync(string email);
        Task<RefreshToken?> IsRefreshTokenValidAsync(WorkshopUser user, string token);
        Task<bool> IsUserValidAsync(AuthenticationRequest credentials);
        Task<int> SaveChangesAsync();
    }
}