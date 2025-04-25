using System;
using System.Collections.Generic;
using System.Linq;
using AccountService.Domain.Entities;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
{
    Task<IReadOnlyList<Vehicle>> GetAvailableVehiclesAsync();
    Task<Vehicle?> GetVehicleWithTypeAsync(int vehicleId);
}
}