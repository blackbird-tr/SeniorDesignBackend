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

        public override async Task<CargoAd> GetByIdAsync(int id)
        {
            return await _context.CargoAds
                .Include(c => c.Customer)  
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public override async Task<IReadOnlyList<CargoAd>> GetAllAsync()
        {
            return await _context.CargoAds
                .Include(c => c.Customer) 
                .ToListAsync();
        }

        public async Task<List<CargoAd>> GetByCustomerIdAsync(string UserId)
        {
            return await _context.CargoAds
                .Include(c => c.Customer) 
                .Where(c => c.UserId == UserId)
                .ToListAsync();
        }

        

         

        public async Task<List<CargoAd>> GetByCargoTypeAsync(string cargoType)
        {
            return await _context.CargoAds
                .Include(c => c.Customer) 
                .Where(c => c.CargoType == cargoType)
                .ToListAsync();
        }
    }
} 