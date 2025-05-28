using AccountService.Application.Features.CargoOffer.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;
using AccountService.Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace AccountService.WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatePrice : BaseApiController
    {
        private readonly IGoogleMapsService _googleMapsService;

        public CalculatePrice(IGoogleMapsService googleMapsService)
        {
            _googleMapsService = googleMapsService;
        }

        [AllowAnonymous]
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalculateModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Google Maps API'den mesafe bilgisini al
                var distanceMatrix = await _googleMapsService.GetDistanceMatrixAsync(
                    $"{model.PickCity}, {model.PickCountry}",
                    $"{model.DeliveryCity}, {model.DeliveryCountry}"
                );

                // Mesafeyi kilometreye çevir
                var distanceInKm = distanceMatrix.Rows[0].Elements[0].Distance.Value / 1000.0;

                // Fiyat hesapla
                var priceResponse = await _googleMapsService.CalculatePriceAsync(
                    $"{model.PickCity}, {model.PickCountry}",
                    $"{model.DeliveryCity}, {model.DeliveryCountry}",
                    model.CargoType,
                    model.Weight,
                    distanceInKm,
                    model.PickCountry
                );

                return Ok(new { 
                    Price = new {
                        Prediction = priceResponse.Prediction,
                        MinPrice = priceResponse.Range[0],
                        MaxPrice = priceResponse.Range[1]
                    },
                    Distance = distanceInKm,
                    Duration = distanceMatrix.Rows[0].Elements[0].Duration.Text,
                    Origin = distanceMatrix.OriginAddresses[0],
                    Destination = distanceMatrix.DestinationAddresses[0]
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        public class CalculateModel
        {
            [Required]
            public string CargoType { get; set; }

            [Required]
            public string PickCity { get; set; }

            [Required]
            public string PickCountry { get; set; }

            [Required]
            public string DeliveryCity { get; set; }

            [Required]
            public string DeliveryCountry { get; set; }

            [Required]
            [Range(0.1, double.MaxValue, ErrorMessage = "Weight must be greater than 0")]
            public double Weight { get; set; }
             
        }
    }
}
