using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.VehicleAd.Queries.GetAll;

namespace AccountService.Application.Features.VehicleAd.Queries.GetByCity
{
    public class GetVehicleAdsByCityQuery : IRequest<List<VehicleAdDto>>
    {
        public string City { get; set; }
    }

    public class GetVehicleAdsByCityQueryHandler : IRequestHandler<GetVehicleAdsByCityQuery, List<VehicleAdDto>>
    {
        private readonly IVehicleAdService _vehicleAdService;

        public GetVehicleAdsByCityQueryHandler(IVehicleAdService vehicleAdService)
        {
            _vehicleAdService = vehicleAdService;
        }

        public async Task<List<VehicleAdDto>> Handle(GetVehicleAdsByCityQuery request, CancellationToken cancellationToken)
        {
            var vehicleAds = await _vehicleAdService.GetByCityAsync(request.City);
            
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