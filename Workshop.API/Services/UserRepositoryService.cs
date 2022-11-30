using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Data;
using Workshop.API.Models;

namespace Workshop.API.Services
{
    public class UserRepositoryService : IUserRepositoryService
    {
        private readonly UserManager<WorkshopUser> _userManager;
        private readonly WorkshopDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public UserRepositoryService(UserManager<WorkshopUser> userManager, WorkshopDbContext dbContext, ITokenService tokenService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        public async Task<bool> CreateUserAsync(RegisterRequest credentials)
        {
            var result = await _userManager.CreateAsync(
            new WorkshopUser { Email = credentials.EmailAddress, UserName = credentials.EmailAddress.Trim(new Char[] { ' ', '@', '.' }) },
                credentials.Password
            );
            return result.Succeeded ? true : false;
        }

        public async Task<WorkshopUser> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<bool> IsUserValidAsync(AuthenticationRequest credentials)
        {
            var user = await _userManager.FindByEmailAsync(credentials.EmailAddress);
            if (user == null)
                return false;
            bool loginSucceded = await _userManager.CheckPasswordAsync(user, credentials.Password);
            return loginSucceded;
        }

        public async Task<RefreshToken> CreateUserRefreshTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = new RefreshToken
            {
                Id = Guid.NewGuid().ToString(),
                User = user,
                Token = _tokenService.GenerateRefreshToken(),
                CreationTime = DateTime.UtcNow,
                IsActive = true
            };
            _dbContext.RefreshToken.Add(token);
            return token;
        }

        public async Task<RefreshToken?> IsRefreshTokenValidAsync(WorkshopUser user, string token)
        {
            var matches = _dbContext.RefreshToken
                .Where(rt => rt.Token.Equals(token) && rt.User.Id == user.Id && rt.IsActive);
            if (matches.Count() != 1) return null;
            return await matches.FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteTokenAsync(string token)
        {
            var refreshToken = await _dbContext.RefreshToken.FirstOrDefaultAsync(rt => rt.Token == token);
            if (refreshToken == null)
                return false;
            _dbContext.RefreshToken.Remove(refreshToken);
            return true;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
