using AccountService.Application.Models;

namespace AccountService.Application.Interfaces
{
    public interface IGoogleMapsService
    {
        Task<DistanceMatrixResponse> GetDistanceMatrixAsync(string origin, string destination);
        Task<PricePredictionResponse> CalculatePriceAsync(string origin, string destination, string cargoType, double weight, double distanceInKm, string country);
    }
} 