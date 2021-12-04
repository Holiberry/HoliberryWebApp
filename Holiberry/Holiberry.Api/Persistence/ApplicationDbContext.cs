using Holiberry.Api.Models.Cities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Holiberry.Api.Models.ServerLogs.Entities;
using Holiberry.Api.Models.Users.Entities.Identity;
using Holiberry.Api.Models.Files.Entities;
using Holiberry.Api.Models.Feats;
using Holiberry.Api.Models.Prizes;
using Holiberry.Api.Models.Quests;
using Holiberry.Api.Models.Rankings;
using Holiberry.Api.Models.Schools;
using Holiberry.Api.Models.Stats;
using Holiberry.Api.Models.Threats;
using Holiberry.Api.Models.Tracks;
using Holiberry.Api.Models.Users;

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


        public DbSet<FeatM> Feats { get; set; }
        public DbSet<FeatConditionM> FeatConditions { get; set; }
        public DbSet<FeatPhotoM> FeatPhotos { get; set; }
        public DbSet<UserFeatM> UserFeats { get; set; }

        public DbSet<PrizeM> Prizes { get; set; }
        public DbSet<PrizePhotoM> PrizePhotos { get; set; }
        public DbSet<UserPrizeM> UserPrizes { get; set; }


        public DbSet<QuestM> Quests { get; set; }
        public DbSet<QuestConditionM> QuestConditions { get; set; }
        public DbSet<UserQuestM> UserQuests { get; set; }


        public DbSet<RankingM> Rankings { get; set; }
        
        
        public DbSet<SchoolM> Schools { get; set; }
        public DbSet<SchoolPhotoM> SchoolPhotos { get; set; }


        public DbSet<UserDailyStatM> UserDailyStats { get; set; }


        public DbSet<ThreatM> Threats { get; set; }
        public DbSet<ThreatPhotoM> ThreatPhotos { get; set; }
        public DbSet<UserThreatM> UserThreats { get; set; }
        public DbSet<UserThreatVoterM> UserThreatVoters { get; set; }

        
        public DbSet<UserTrackM> UserTracks { get; set; }


        public DbSet<UserHomeM> UserHomes { get; set; }
        public DbSet<UserAddressM> UserAddresses { get; set; }


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


            modelBuilder.Entity<SchoolM>()
                .HasOne(a => a.Photo)
                .WithOne()
                .HasForeignKey<SchoolM>(a => a.PhotoId);


            modelBuilder.Entity<FeatM>()
                .HasOne(a => a.Photo)
                .WithOne()
                .HasForeignKey<FeatM>(a => a.PhotoId);


            modelBuilder.Entity<PrizeM>()
                .HasOne(a => a.Photo)
                .WithOne()
                .HasForeignKey<PrizeM>(a => a.PhotoId);


            modelBuilder.Entity<ThreatM>()
                .HasOne(a => a.Photo)
                .WithOne()
                .HasForeignKey<ThreatM>(a => a.PhotoId);


            modelBuilder.Entity<UserM>()
                .HasOne(a => a.OtherAddress)
                .WithOne()
                .HasForeignKey<UserM>(a => a.OtherAddressId);

            modelBuilder.Entity<UserM>()
                .HasOne(a => a.Home)
                .WithOne()
                .HasForeignKey<UserM>(a => a.HomeId);

        }
    }
}
