using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.VehicleAd.Queries.GetAll;

namespace AccountService.Application.Features.VehicleAd.Queries.GetByCountry
{
    public class GetVehicleAdsByCountryQuery : IRequest<List<VehicleAdDto>>
    {
        public string Country { get; set; }
    }

    public class GetVehicleAdsByCountryQueryHandler : IRequestHandler<GetVehicleAdsByCountryQuery, List<VehicleAdDto>>
    {
        private readonly IVehicleAdService _vehicleAdService;

        public GetVehicleAdsByCountryQueryHandler(IVehicleAdService vehicleAdService)
        {
            _vehicleAdService = vehicleAdService;
        }

        public async Task<List<VehicleAdDto>> Handle(GetVehicleAdsByCountryQuery request, CancellationToken cancellationToken)
        {
            var vehicleAds = await _vehicleAdService.GetByCountryAsync(request.Country);
            
            return vehicleAds.Select(ad => new VehicleAdDto
            {
                Id = ad.Id,
                Title = ad.Title,
                Description = ad.Desc,
                Country = ad.Country,
                City = ad.City,
                CarrierId = ad.userId,
                CarrierName = ad.Carrier.UserName,
                VehicleType = ad.VehicleType,
                Capacity = ad.Capacity,
                CreatedDate = ad.CreatedDate
            }).ToList();
        }
    }
} 