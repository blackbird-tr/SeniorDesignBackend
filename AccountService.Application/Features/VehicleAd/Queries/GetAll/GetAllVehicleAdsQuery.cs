using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.VehicleAd.Queries.GetAll
{
    public class GetAllVehicleAdsQuery : IRequest<List<VehicleAdDto>> { }

    public class GetAllVehicleAdsQueryHandler : IRequestHandler<GetAllVehicleAdsQuery, List<VehicleAdDto>>
    {
        private readonly IVehicleAdService _vehicleAdService;

        public GetAllVehicleAdsQueryHandler(IVehicleAdService vehicleAdService)
        {
            _vehicleAdService = vehicleAdService;
        }

        public async Task<List<VehicleAdDto>> Handle(GetAllVehicleAdsQuery request, CancellationToken cancellationToken)
        {
            var vehicleAds = await _vehicleAdService.GetAllAsync();

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