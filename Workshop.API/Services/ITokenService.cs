using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Workshop.API.Services
{
    public interface ITokenService
    {
        string GenerateToken(IdentityUser user, Claim[] userClaims);
    }
}