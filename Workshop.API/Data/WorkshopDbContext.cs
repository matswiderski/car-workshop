using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Workshop.API.Data
{
    public class WorkshopDbContext : IdentityDbContext<IdentityUser>
    {
        public WorkshopDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
