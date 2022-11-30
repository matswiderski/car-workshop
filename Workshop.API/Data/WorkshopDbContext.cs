using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Models;

namespace Workshop.API.Data
{
    public class WorkshopDbContext : IdentityDbContext<WorkshopUser>
    {
        public WorkshopDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<RefreshToken> RefreshToken { get; set; }
    }
}
