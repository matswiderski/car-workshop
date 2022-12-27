using System.Security.Claims;
using Workshop.API.Models;

namespace Workshop.API.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(AuthenticationRequest credentials, string id);
        string GenerateRefreshToken();
        ClaimsPrincipal GetClaimsPrincipalFromToken(string token);
    }
}