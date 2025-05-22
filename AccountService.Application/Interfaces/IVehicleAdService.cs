using AccountService.Domain.Entities;

namespace AccountService.Application.Interfaces
{
    public interface IVehicleAdService : IGenericRepository<Domain.Entities.VehicleAd>
    {
        Task<List<VehicleAd>> GetByCarrierIdAsync(string UserId);
        Task<List<VehicleAd>> GetByVehicleTypeAsync(string vehicleType);
        Task<List<VehicleAd>> GetByPickUpLocationAsync(int pickUpLocationId);
        Task<IReadOnlyList<VehicleAd>> GetAllAsync();
        Task<VehicleAd> GetByIdAsync(int id);
    }
} 