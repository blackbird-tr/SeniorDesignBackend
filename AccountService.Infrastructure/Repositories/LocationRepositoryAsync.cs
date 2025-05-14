using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Repositories
{
    public class LocationRepositoryAsync : GenericRepository<Location>, ILocationService
    {
        private readonly ApplicationDbContext _context;

        public LocationRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<Location?> GetByFullAddressAsync(string? address, string? city, string? state, int? postalCode)
        {
            return await _context.Locations
                .Where(l => l.Active &&
                            l.Address == address &&
                            l.City == city &&
                            l.State == state &&
                            l.PostalCode == postalCode)
                .FirstOrDefaultAsync();
        }

        public async Task<Location?> GetByCoordinatesAsync(string coordinates)
        {
            return await _context.Locations
                .Where(l => l.Active && l.Coordinates == coordinates)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Location>> GetByCityAsync(string city)
        {
            return await _context.Locations
                .Where(l => l.Active && l.City == city)
                .ToListAsync();
        }

        public async Task<List<Location>> GetByPostalCodeAsync(int postalCode)
        {
            return await _context.Locations
                .Where(l => l.Active && l.PostalCode == postalCode)
                .ToListAsync();
        }

        public async Task<List<Location>> GetByRegionAsync(string city, string state)
        {
            return await _context.Locations
                .Where(l => l.Active && l.City == city && l.State == state)
                .ToListAsync();
        }
    }
}
