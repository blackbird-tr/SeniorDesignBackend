using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Repositories
{
    public class BookingRepositoryAsync : GenericRepository<Booking>, IBookingService
    {
        private readonly ApplicationDbContext _context;

        public BookingRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Booking>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Bookings
                .Where(b => b.Active && b.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetByCarrierIdAsync(int carrierId)
        {
            return await _context.Bookings
                .Where(b => b.Active && b.CarrierId == carrierId)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetByVehicleIdAsync(int vehicleId)
        {
            return await _context.Bookings
                .Where(b => b.Active && b.VehicleId == vehicleId)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetByCargoIdAsync(int cargoId)
        {
            return await _context.Bookings
                .Where(b => b.Active && b.CargoId == cargoId)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetByStatusAsync(byte status)
        {
            return await _context.Bookings
                .Where(b => b.Active && b.Status == status)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetByPickupDateRangeAsync(DateTime start, DateTime end)
        {
            return await _context.Bookings
                .Where(b => b.Active && b.PickupDate >= start && b.PickupDate <= end)
                .ToListAsync();
        }
    }
}
