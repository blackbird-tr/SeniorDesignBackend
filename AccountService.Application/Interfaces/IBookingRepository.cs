using AccountService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
{
    Task<IReadOnlyList<Booking>> GetBookingsByCustomerIdAsync(int customerId);
    Task<Booking?> GetDetailedBookingAsync(int bookingId);
}
}