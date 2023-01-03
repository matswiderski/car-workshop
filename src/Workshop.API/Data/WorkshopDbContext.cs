using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Extensions;
using Workshop.API.Models;

namespace Workshop.API.Data
{
    public class WorkshopDbContext : IdentityDbContext<WorkshopUser>
    {
        private static IWebHostEnvironment _env;
        public DbSet<WorkshopUser> WorkshopUsers { get; set; }
        public DbSet<BusinessUser> BusinessUsers { get; set; }
        public DbSet<PersonalUser> PersonalUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Models.Workshop> Workshops { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<WorkshopService> WorkshopServices { get; set; }
        public DbSet<RepairService> RepairServices { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Localization> Localizations { get; set; }

        public WorkshopDbContext(DbContextOptions options, IWebHostEnvironment env) : base(options)
        {
            _env = env;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessUser>()
                .HasMany(bu => bu.Workshops)
                .WithOne(w => w.Owner)
                .HasForeignKey(w => w.OwnerId);
            modelBuilder.Entity<WorkshopUser>()
                .HasMany(wu => wu.RefreshTokens)
                .WithOne(rt => rt.User);

            modelBuilder.Entity<WorkshopService>()
                .HasKey(ws => new { ws.ServiceId, ws.WorkshopId });
            modelBuilder.Entity<WorkshopService>()
                .HasOne(ws => ws.Service)
                .WithMany(s => s.WorkshopServices)
                .HasForeignKey(ws => ws.ServiceId);
            modelBuilder.Entity<WorkshopService>()
                .HasOne(ws => ws.Workshop)
                .WithMany(w => w.WorkshopServices)
                .HasForeignKey(ws => ws.WorkshopId);

            modelBuilder.Entity<Models.Workshop>()
                .HasOne(w => w.Localization)
                .WithOne(l => l.Workshop)
                .HasForeignKey<Localization>(l => l.WorkshopId);

            modelBuilder.Entity<Models.Workshop>()
                .HasMany(w => w.Repairs)
                .WithOne(r => r.Workshop)
                .HasForeignKey(r => r.WorkshopId);

            modelBuilder.Entity<PersonalUser>()
                .HasMany(pu => pu.Cars)
                .WithOne(c => c.PersonalUser)
                .HasForeignKey(c => c.PersonalUserId);

            modelBuilder.Entity<PersonalUser>()
                .HasMany(pu => pu.Repairs)
                .WithOne(r => r.PersonalUser)
                .HasForeignKey(r => r.PersonalUserId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<RepairService>()
                .HasKey(rs => new { rs.ServiceId, rs.RepairId });
            modelBuilder.Entity<RepairService>()
                .HasOne(rs => rs.Service)
                .WithMany(s => s.RepairServices)
                .HasForeignKey(ws => ws.ServiceId);
            modelBuilder.Entity<RepairService>()
                .HasOne(rs => rs.Repair)
                .WithMany(r => r.RepairServices)
                .HasForeignKey(rs => rs.RepairId);

            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }

    }
}
