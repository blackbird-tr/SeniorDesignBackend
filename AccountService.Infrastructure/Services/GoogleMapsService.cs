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
        private readonly string _mlApiUrl = "https://price-prediction-for-freights-yj6h.onrender.com/predict";

        // Kargo tipi bazlı değerler
        private readonly Dictionary<CargoType, (string complexity, double optimalLoad, double maxLegalLoad, string vehicleType)> _cargoTypeValues = new()
        {
            { CargoType.General, ("low", 20.0, 24.0, "Tarpaulin Truck") },        // TarpaulinTruck
            { CargoType.Fragile, ("high", 15.0, 18.0, "Box Truck") },        // BoxTruck
            { CargoType.Refrigerated, ("high", 18.0, 22.0, "Refrigerated Truck") },   // RefrigeratedTruck
            { CargoType.Oversized, ("high", 30.0, 36.0, "Semi Trailer") },      // SemiTrailer
            { CargoType.LightFreight, ("low", 8.0, 10.0, "Light Truck") },    // LightTruck
            { CargoType.Containerized, ("high", 25.0, 30.0, "Container Carrier") },  // ContainerCarrier
            { CargoType.Liquid, ("high", 22.0, 26.0, "Tank Truck") },         // TankTruck
            { CargoType.HeavyMachinery, ("high", 35.0, 40.0, "Lowbed Trailer") }, // LowbedTrailer
            { CargoType.Construction, ("high", 28.0, 32.0, "Dump Truck") },   // DumpTruck
            { CargoType.Parcel, ("low", 5.0, 6.0, "Panel Van") },           // PanelVan
            { CargoType.Others, ("low", 15.0, 18.0, "General Truck") }          // Others
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

        public async Task<PricePredictionResponse> CalculatePriceAsync(string origin, string destination, string cargoType, double weight, double distanceInKm, string country)
        {
            // String cargo type'ı enum'a çevir
            if (!Enum.TryParse<CargoType>(cargoType, true, out var cargoTypeEnum))
            {
                cargoTypeEnum = CargoType.Others;
            }

            // Kargo tipine göre değerleri al
            var (complexity, optimalLoad, maxLegalLoad, vehicleType) = _cargoTypeValues[cargoTypeEnum];

            //// Yük ağırlığı kontrolü
            //if (weight > maxLegalLoad)
            //{
            //    throw new ArgumentException($"Weight exceeds maximum legal load for {cargoType} cargo type");
            //}

            // Google Maps API'den süre bilgisini al
            var distanceMatrix = await GetDistanceMatrixAsync(origin, destination);
            var durationInHours = distanceMatrix.Rows[0].Elements[0].Duration.Value / 3600.0; // Saniyeyi saate çevir

            // ML API'ye gönderilecek request modeli
            var requestModel = new
            {
                country = country,
                vehicle = vehicleType,
                complexity_factor = complexity,
                distance_km = distanceInKm,
                duration_hr = durationInHours,
                cargo_ton = weight,
                optimal_load_ton = optimalLoad,
                max_legal_ton = maxLegalLoad
            };

            // ML API'ye istek at
            var jsonContent = JsonSerializer.Serialize(requestModel);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync(_mlApiUrl, content);
            response.EnsureSuccessStatusCode();
            
            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            return JsonSerializer.Deserialize<PricePredictionResponse>(responseContent, options);
        }
    }
} 