using AccountService.Application.Interfaces;
using AccountService.Application.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace AccountService.Infrastructure.Services
{
    public class GoogleMapsService : IGoogleMapsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IConfiguration _configuration;

        // Kargo tipi bazlı değerler
        private readonly Dictionary<CargoType, (string complexity, double optimalLoad, double maxLegalLoad)> _cargoTypeValues = new()
        {
            { CargoType.General, ("low", 20.0, 24.0) },        // TarpaulinTruck
            { CargoType.Fragile, ("high", 15.0, 18.0) },        // BoxTruck
            { CargoType.Refrigerated, ("high", 18.0, 22.0) },   // RefrigeratedTruck
            { CargoType.Oversized, ("high", 30.0, 36.0) },      // SemiTrailer
            { CargoType.LightFreight, ("low", 8.0, 10.0) },    // LightTruck
            { CargoType.Containerized, ("high", 25.0, 30.0) },  // ContainerCarrier
            { CargoType.Liquid, ("high", 22.0, 26.0) },         // TankTruck
            { CargoType.HeavyMachinery, ("high", 35.0, 40.0) }, // LowbedTrailer
            { CargoType.Construction, ("high", 28.0, 32.0) },   // DumpTruck
            { CargoType.Parcel, ("low", 5.0, 6.0) },           // PanelVan
            { CargoType.Others, ("low", 15.0, 18.0) }          // Others
        };

        public GoogleMapsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiKey = _configuration["GoogleMaps:ApiKey"];
        }

        public async Task<DistanceMatrixResponse> GetDistanceMatrixAsync(string origin, string destination)
        {
            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={Uri.EscapeDataString(origin)}&destinations={Uri.EscapeDataString(destination)}&key={_apiKey}";
            
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            var result = JsonSerializer.Deserialize<DistanceMatrixResponse>(content, options);
            
            if (result.Status != "OK")
            {
                throw new Exception($"Google Maps API error: {result.ErrorMessage}");
            }
            
            return result;
        }

        public async Task<double> CalculatePriceAsync(string cargoType, double weight, double distanceInKm)
        {
            // String cargo type'ı enum'a çevir
            if (!Enum.TryParse<CargoType>(cargoType, true, out var cargoTypeEnum))
            {
                cargoTypeEnum = CargoType.Others;
            }

            // Kargo tipine göre değerleri al
            var (defaultComplexity, optimalLoad, maxLegalLoad) = _cargoTypeValues[cargoTypeEnum];

            // Temel fiyat hesaplama (TL/km)
            var basePricePerKm = 2.0;

            // Karmaşıklık faktörüne göre fiyat artışı
            //// Eğer kullanıcı complexity_factor belirtmemişse, kargo tipinin varsayılan değerini kullan
            //var complexityMultiplier = string.IsNullOrEmpty(complexity_factor) 
            //    ? (defaultComplexity == "high" ? 1.5 : 1.2)
            //    : complexity_factor.ToLower() switch
            //    {
            //        "high" => 1.5,
            //        "low" => 1.2,
            //        _ => 1.3 // varsayılan değer
            //    };

            //// Ağırlık optimizasyonu
            var weightEfficiency = weight <= optimalLoad ? 1.0 : 
                                 weight <= maxLegalLoad ? 1.2 : 1.5;

            // Mesafe bazlı çarpan (her 100km için %5 artış)
            var distanceMultiplier = 1 + (distanceInKm / 100) * 0.05;

            // Yük ağırlığı kontrolü
            if (weight > maxLegalLoad)
            {
                throw new ArgumentException($"Weight exceeds maximum legal load for {cargoType} cargo type");
            }

            // Fiyat hesaplama
            //var price = basePricePerKm * distanceInKm * complexityMultiplier * weightEfficiency * distanceMultiplier;

            return 12;
        }
    }
} 