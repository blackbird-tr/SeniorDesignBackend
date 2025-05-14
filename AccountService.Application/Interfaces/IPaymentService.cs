using AccountService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface IPaymentService:IGenericRepository<Payment>
    {
        Task<List<Payment>> GetByBookingIdAsync(int bookingId);
        Task<List<Payment>> GetByStatusAsync(byte status);
        Task<List<Payment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<float?> GetTotalPaidAmountByBookingIdAsync(int bookingId);
    }
}
