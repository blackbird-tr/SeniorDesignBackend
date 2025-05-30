using AccountService.Domain.Entities;

namespace AccountService.Application.Interfaces
{
    public interface ICargoAdService : IGenericRepository<Domain.Entities.CargoAd>
    {
        Task<List<CargoAd>> GetByCustomerIdAsync(string UserId); 
        Task<List<CargoAd>> GetByCargoTypeAsync(string cargoType);
        Task<List<CargoAd>> GetByPickCityAsync(string city);
        Task<List<CargoAd>> GetByPickCountryAsync(string country);
        Task<List<CargoAd>> GetByDropCityAsync(string city);
        Task<List<CargoAd>> GetByDropCountryAsync(string country);
    }
} 