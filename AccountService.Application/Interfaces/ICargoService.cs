using AccountService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface ICargoService:IGenericRepository<Domain.Entities.Cargo>
    {
        Task<List<Cargo>> GetByCustomerIdAsync(int customerId);
        Task<List<Cargo>> GetByStatusAsync(byte status);  
        Task<List<Cargo>> GetByPickupLocationIdAsync(int pickupLocationId);
        Task<List<Cargo>> GetByDropoffLocationIdAsync(int dropoffLocationId);
        Task<List<Cargo>> GetByCargoTypeAsync(string cargoType);
        Task<List<Cargo>> GetByWeightRangeAsync(float minWeight, float maxWeight);
    }
}
