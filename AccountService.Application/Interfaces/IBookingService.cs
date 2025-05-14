using AccountService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface IBookingService : IGenericRepository<Booking>
    {
        Task<List<Booking>> GetByCustomerIdAsync(int customerId);
        Task<List<Booking>> GetByCarrierIdAsync(int carrierId);
        Task<List<Booking>> GetByVehicleIdAsync(int vehicleId);
        Task<List<Booking>> GetByCargoIdAsync(int cargoId);
        Task<List<Booking>> GetByStatusAsync(byte status);
        Task<List<Booking>> GetByPickupDateRangeAsync(DateTime start, DateTime end);
    }
}
