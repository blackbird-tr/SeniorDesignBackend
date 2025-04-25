using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AccountService.Domain.Entities;
namespace AccountService.Application.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
{
    Task<Payment?> GetLatestPaymentForBookingAsync(int bookingId);
}
}