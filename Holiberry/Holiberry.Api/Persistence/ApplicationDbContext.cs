using Holiberry.Api.Models.Cities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Holiberry.Api.Models.ServerLogs.Entities;
using Holiberry.Api.Models.Users.Entities.Identity;
using Holiberry.Api.Models.Files.Entities;

namespace Holiberry.Api.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<UserM, RoleM, long, UserClaimM, UserRoleM, UserLoginM, IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public void Migrate()
        {
            Database.Migrate();
        }

        public DbSet<ServerLogM> ServerLogs { get; set; }
        public DbSet<CityM> Cities { get; set; }
        
        
        public DbSet<DbFileM> DbFiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ServerLogM>()
                .Property(a => a.Message).HasMaxLength(8000);

            modelBuilder.Entity<ServerLogM>()
                .Property(a => a.InnerMessage).HasMaxLength(8000);

            modelBuilder.Entity<ServerLogM>()
                .Property(a => a.StackTrace).HasMaxLength(8000);

            modelBuilder.Entity<ServerLogM>()
                .Property(a => a.RequestBody).HasMaxLength(4000);

            modelBuilder.Entity<ServerLogM>()
                .Property(a => a.QueryString).HasMaxLength(1000);


            //--------------------CONFIGURE IDENTITY--------------------//
            modelBuilder.Entity<UserM>()
                .HasMany(e => e.UserRoles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<UserRoleM>()
                .HasOne(e => e.User)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<UserRoleM>()
                .HasOne(e => e.Role)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.RoleId);
            //-- End of Identity Roles Configuration

            //Identity Claims Configuration
            modelBuilder.Entity<UserM>()
                  .HasMany(e => e.Claims)
                  .WithOne(a => a.User)
                  .HasForeignKey(e => e.UserId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);

            ////-- End of Identity Claims Configuration
            modelBuilder.HasPostgresExtension("postgis");

        }
    }
}
