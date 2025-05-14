using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Repositories
{
    public class CargoRepositoryAsync : GenericRepository<Cargo>, ICargoService
    {
        private readonly ApplicationDbContext _context;

        public CargoRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Cargo>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Cargos
                .Where(c => c.Active && c.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<List<Cargo>> GetByStatusAsync(byte status)
        {
            return await _context.Cargos
                .Where(c => c.Active && c.Status == status)
                .ToListAsync();
        }

        public async Task<List<Cargo>> GetByPickupLocationIdAsync(int pickupLocationId)
        {
            return await _context.Cargos
                .Where(c => c.Active && c.PickupLocationId == pickupLocationId)
                .ToListAsync();
        }

        public async Task<List<Cargo>> GetByDropoffLocationIdAsync(int dropoffLocationId)
        {
            return await _context.Cargos
                .Where(c => c.Active && c.DropoffLocationId == dropoffLocationId)
                .ToListAsync();
        }

        public async Task<List<Cargo>> GetByCargoTypeAsync(string cargoType)
        {
            return await _context.Cargos
                .Where(c => c.Active && c.CargoType != null && c.CargoType.ToLower() == cargoType.ToLower())
                .ToListAsync();
        }

        public async Task<List<Cargo>> GetByWeightRangeAsync(float minWeight, float maxWeight)
        {
            return await _context.Cargos
                .Where(c => c.Active && c.Weight >= minWeight && c.Weight <= maxWeight)
                .ToListAsync();
        }
    }
}
