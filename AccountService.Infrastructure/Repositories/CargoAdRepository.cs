using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context; 
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Repositories
{
    public class CargoAdRepository : GenericRepository<CargoAd>, ICargoAdService
    {
        private readonly ApplicationDbContext _context;

        public CargoAdRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<CargoAd>> GetByCustomerIdAsync(string UserId)
        {
            return await _context.CargoAds
                .Where(c => c.UserId == UserId)
                .ToListAsync();
        }

        public async Task<List<CargoAd>> GetByPickupLocationAsync(int pickupLocationId)
        {
            return await _context.CargoAds
                .Where(c => c.PickupLocationId == pickupLocationId)
                .ToListAsync();
        }

        public async Task<List<CargoAd>> GetByDropoffLocationAsync(int dropoffLocationId)
        {
            return await _context.CargoAds
                .Where(c => c.DropoffLocationId == dropoffLocationId)
                .ToListAsync();
        }

        public async Task<List<CargoAd>> GetByCargoTypeAsync(string cargoType)
        {
            return await _context.CargoAds
                .Where(c => c.CargoType == cargoType)
                .ToListAsync();
        }
    }
} 