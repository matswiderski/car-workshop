using Azure.Core;
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
        public UserRepositoryService(UserManager<WorkshopUser> userManager,
            WorkshopDbContext dbContext,
            ITokenService tokenService)
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
            => await _userManager.FindByEmailAsync(email);

        public async Task<WorkshopUser> GetUserByIdAsync(string id)
             => await _userManager.FindByIdAsync(id);

        public async Task<bool> ValidateEmailConfirmAsync(WorkshopUser user)
            => await _userManager.IsEmailConfirmedAsync(user);

        public async Task<bool> ConfirmEmailAsync(WorkshopUser user, string token)
        {
            var confirm = await _userManager.ConfirmEmailAsync(user, token);
            return confirm.Succeeded;
        }

        public async Task<bool> IsUserValidAsync(AuthenticationRequest credentials)
        {
            var user = await _userManager.FindByEmailAsync(credentials.EmailAddress);
            bool emailConfirmed = await ValidateEmailConfirmAsync(user);
            if (user == null || !emailConfirmed)
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

        public async Task<bool> DeleteRefreshTokenAsync(string token)
        {
            var refreshToken = await _dbContext.RefreshToken.FirstOrDefaultAsync(rt => rt.Token == token);
            if (refreshToken == null)
                return false;
            _dbContext.RefreshToken.Remove(refreshToken);
            return true;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(WorkshopUser user)
            => await _userManager.GenerateEmailConfirmationTokenAsync(user);

        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
    }
}
