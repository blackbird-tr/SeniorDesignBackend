using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Repositories
{
    public class VehicleAdRepositoryAsync : GenericRepository<VehicleAd>, IVehicleAdService
    {
        private readonly ApplicationDbContext _context;

        public VehicleAdRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<VehicleAd>> GetByCarrierIdAsync(string UserId)
        {
            return await _context.VehicleAds
                .Where(v => v.userId == UserId && v.Active)
                .ToListAsync();
        }

        public async Task<List<VehicleAd>> GetByVehicleTypeAsync(string vehicleType)
        {
            return await _context.VehicleAds
                .Where(v => v.VehicleType == vehicleType && v.Active)
                .ToListAsync();
        }

        public async Task<List<VehicleAd>> GetByPickUpLocationAsync(int pickUpLocationId)
        {
            return await _context.VehicleAds
                .Where(v => v.PickUpLocationId == pickUpLocationId && v.Active)
                .ToListAsync();
        }
    }
} 