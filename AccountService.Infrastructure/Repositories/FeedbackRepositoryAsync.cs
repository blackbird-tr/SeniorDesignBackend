using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Repositories
{
    public class FeedbackRepositoryAsync : GenericRepository<Feedback>, IFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public FeedbackRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Feedback>> GetByBookingIdAsync(int bookingId)
        {
            return await _context.Feedbacks
                .Where(f => f.Active && f.BookingId == bookingId)
                .ToListAsync();
        }

        public async Task<List<Feedback>> GetByUserIdAsync(string userId)
        {
            return await _context.Feedbacks
                .Where(f => f.Active && f.UserId == userId)
                .ToListAsync();
        }

        public async Task<float?> GetAverageRatingByBookingIdAsync(int bookingId)
        {
            return await _context.Feedbacks
                .Where(f => f.Active && f.BookingId == bookingId && f.Rating.HasValue)
                .AverageAsync(f => (float?)f.Rating); 
        }
    }
}
