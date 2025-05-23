using AccountService.Domain.Entities;

namespace AccountService.Application.Interfaces
{
    public interface ICargoAdService : IGenericRepository<Domain.Entities.CargoAd>
    {
        Task<List<CargoAd>> GetByCustomerIdAsync(string UserId); 
        Task<List<CargoAd>> GetByCargoTypeAsync(string cargoType);
    }
} 