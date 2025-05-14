using AccountService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface IFeedbackService : IGenericRepository<Feedback>
    {
        Task<List<Feedback>> GetByBookingIdAsync(int bookingId);
        Task<List<Feedback>> GetByUserIdAsync(string userId);
        Task<float?> GetAverageRatingByBookingIdAsync(int bookingId);
    }
}
