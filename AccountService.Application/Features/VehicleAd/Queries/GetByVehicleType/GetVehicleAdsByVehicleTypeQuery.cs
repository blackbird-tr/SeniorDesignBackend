using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.VehicleAd.Queries.GetAll;

namespace AccountService.Application.Features.VehicleAd.Queries.GetByVehicleType
{
    public class GetVehicleAdsByVehicleTypeQuery : IRequest<List<VehicleAdDto>>
    {
        public string VehicleType { get; set; }
    }

    public class GetVehicleAdsByVehicleTypeQueryHandler : IRequestHandler<GetVehicleAdsByVehicleTypeQuery, List<VehicleAdDto>>
    {
        private readonly IVehicleAdService _vehicleAdService;

        public GetVehicleAdsByVehicleTypeQueryHandler(IVehicleAdService vehicleAdService)
        {
            _vehicleAdService = vehicleAdService;
        }

        public async Task<List<VehicleAdDto>> Handle(GetVehicleAdsByVehicleTypeQuery request, CancellationToken cancellationToken)
        {
            var vehicleAds = await _vehicleAdService.GetByVehicleTypeAsync(request.VehicleType);

            return vehicleAds
                .Where(x => x.Active)
                .Select(ad => new VehicleAdDto
                {
                    Id = ad.Id,
                    Title = ad.Title,
                    Description = ad.Desc,
                    PickUpLocationId = ad.PickUpLocationId,
                    CarrierId = ad.userId,
                    CarrierName = ad.Carrier.UserName,
                    VehicleType = ad.VehicleType,
                    Capacity = ad.Capacity,
                    CreatedDate = ad.CreatedDate
                })
                .ToList();
        }
    }
} 