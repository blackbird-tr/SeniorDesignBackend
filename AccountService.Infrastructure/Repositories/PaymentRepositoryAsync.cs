using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Repositories
{
    public class PaymentRepositoryAsync : GenericRepository<Payment>, IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Payment>> GetByBookingIdAsync(int bookingId)
        {
            return await _context.Payments
                .Where(p => p.Active && p.BookingId == bookingId)
                .ToListAsync();
        }

        public async Task<List<Payment>> GetByStatusAsync(byte status)
        {
            return await _context.Payments
                .Where(p => p.Active && p.Status == status)
                .ToListAsync();
        }

        public async Task<List<Payment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Payments
                .Where(p => p.Active && p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                .ToListAsync();
        }

        public async Task<float?> GetTotalPaidAmountByBookingIdAsync(int bookingId)
        {
            return await _context.Payments
                .Where(p => p.Active && p.BookingId == bookingId)
                .SumAsync(p => (float?)p.Amount); // nullable olarak döner
        }
    }
}
