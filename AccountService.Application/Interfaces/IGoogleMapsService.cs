using AccountService.Application.Models;

namespace AccountService.Application.Interfaces
{
    public interface IGoogleMapsService
    {
        Task<DistanceMatrixResponse> GetDistanceMatrixAsync(string origin, string destination);
        Task<double> CalculatePriceAsync(string cargoType, double weight, double distanceInKm );
    }
} 