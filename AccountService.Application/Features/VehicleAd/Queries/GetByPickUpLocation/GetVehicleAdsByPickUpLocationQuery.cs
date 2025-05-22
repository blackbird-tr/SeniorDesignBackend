using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.VehicleAd.Queries.GetAll;

namespace AccountService.Application.Features.VehicleAd.Queries.GetByPickUpLocation
{
    public class GetVehicleAdsByPickUpLocationQuery : IRequest<List<VehicleAdDto>>
    {
        public int PickUpLocationId { get; set; }
    }

    public class GetVehicleAdsByPickUpLocationQueryHandler : IRequestHandler<GetVehicleAdsByPickUpLocationQuery, List<VehicleAdDto>>
    {
        private readonly IVehicleAdService _vehicleAdService;

        public GetVehicleAdsByPickUpLocationQueryHandler(IVehicleAdService vehicleAdService)
        {
            _vehicleAdService = vehicleAdService;
        }

        public async Task<List<VehicleAdDto>> Handle(GetVehicleAdsByPickUpLocationQuery request, CancellationToken cancellationToken)
        {
            var vehicleAds = await _vehicleAdService.GetByPickUpLocationAsync(request.PickUpLocationId);

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