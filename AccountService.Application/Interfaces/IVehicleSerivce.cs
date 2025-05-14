using AccountService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface IVehicleSerivce:IGenericRepository<Domain.Entities.Vehicle>
    {
        Task<List<Vehicle>> GetByCarrierIdAsync(int carrierId);
        Task<List<Vehicle>> GetAvailableVehiclesAsync();  
        Task<Vehicle?> GetByLicensePlateAsync(string licensePlate);
        Task<List<Vehicle>> GetByVehicleTypeIdAsync(int vehicleTypeId);
    }
}
