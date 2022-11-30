using System.Security.Claims;
using Workshop.API.Models;

namespace Workshop.API.Services
{
    public interface ITokenService
    {
        string GenerateAccesToken(AuthenticationRequest credentials);
        string GenerateRefreshToken();
        ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token);
    }
}