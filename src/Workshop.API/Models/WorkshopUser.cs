using Microsoft.AspNetCore.Identity;

namespace Workshop.API.Models
{
    public class WorkshopUser : IdentityUser
    {
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
