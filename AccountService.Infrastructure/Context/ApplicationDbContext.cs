using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AccountService.Infrastructure.Extensions;

namespace AccountService.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTimeService,
            IAuthenticatedUserService authenticatedUserService)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTimeService = dateTimeService;
            _authenticatedUserService = authenticatedUserService;
        }
     
        // DbSet Properties
        public DbSet<User> Users { get; set; }
        
        public DbSet<CargoAd> CargoAds { get; set; } 
        public DbSet<Location> Locations { get; set; }
        public DbSet<Notification> Notifications { get; set; } 
        public DbSet<Vehicle> Vehicles { get; set; } 
        public DbSet<VehicleAd> VehicleAds { get; set; } 
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = _dateTimeService.NowUtc;
                        entry.Entity.AddedBy = _authenticatedUserService.UserId;
                        entry.Entity.Active = true;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = _dateTimeService.NowUtc;
                        entry.Entity.UpdatedBy = _authenticatedUserService.UserId;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AppendQueryFilter<IBaseEntity>(x => x.Active);


            base.OnModelCreating(modelBuilder); // Identity tabloları için şart!

            

             

            modelBuilder.Entity<CargoAd>()
                .HasOne(c => c.PickupLocation)
                .WithMany()
                .HasForeignKey(c => c.PickupLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CargoAd>()
                .HasOne(c => c.DropoffLocation)
                .WithMany()
                .HasForeignKey(c => c.DropoffLocationId)
                .OnDelete(DeleteBehavior.Restrict);
        


            // 2. Tüm foreign key ilişkilerinde default olarak Restrict uygula
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
