using AccountService.Domain.Entities;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface IVehicleTypeService : IGenericRepository<VehicleType>
    {
        Task<List<VehicleType>> GetAllVehicleTypesAsync();
    }
} 