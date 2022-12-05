using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Models;

namespace Workshop.API.Data
{
    public class WorkshopDbContext : IdentityDbContext<WorkshopUser>
    {

        public DbSet<WorkshopUser> WorkshopUsers { get; set; }
        public DbSet<BusinessUser> BusinessUsers { get; set; }
        public DbSet<PersonalUser> PersonalUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public WorkshopDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessUser>().ToTable("BusinessUsers");
            modelBuilder.Entity<PersonalUser>().ToTable("PersonalUsers");
            modelBuilder.Entity<BusinessUser>()
                .HasOne(bu => bu.Localization)
                .WithOne(l => l.BusinessUser)
                .HasForeignKey<Localization>(l => l.BusinessUserId);
            modelBuilder.Entity<WorkshopUser>()
                .HasMany(wu => wu.RefreshTokens)
                .WithOne(rt => rt.User);
            base.OnModelCreating(modelBuilder);
        }

    }
}
