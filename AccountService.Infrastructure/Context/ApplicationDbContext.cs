using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

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
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.AddedDate = _dateTimeService.NowUtc;
                        entry.Entity.AddedBy = _authenticatedUserService.UserId;
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
            base.OnModelCreating(modelBuilder); // Identity tabloları için şart!

            // 1. Vehicle -> VehicleType ilişkisinde Restrict
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasOne(v => v.VehicleType)
                      .WithMany(vt => vt.Vehicles)
                      .HasForeignKey(v => v.VehicleTypeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 2. Tüm foreign key ilişkilerinde default olarak Restrict uygula
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
