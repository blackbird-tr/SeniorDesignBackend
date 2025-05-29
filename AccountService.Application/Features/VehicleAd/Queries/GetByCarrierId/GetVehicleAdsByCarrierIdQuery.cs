using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.VehicleAd.Queries.GetAll;

namespace AccountService.Application.Features.VehicleAd.Queries.GetByCarrierId
{
    public class GetVehicleAdsByCarrierIdQuery : IRequest<List<VehicleAdDto>>
    {
        public string CarrierId { get; set; }
        public byte? Status { get; set; } // ✅ Status parametresi eklendi
    }

    public class GetVehicleAdsByCarrierIdQueryHandler : IRequestHandler<GetVehicleAdsByCarrierIdQuery, List<VehicleAdDto>>
    {
        private readonly IVehicleAdService _vehicleAdService;

        public GetVehicleAdsByCarrierIdQueryHandler(IVehicleAdService vehicleAdService)
        {
            _vehicleAdService = vehicleAdService;
        }

        public async Task<List<VehicleAdDto>> Handle(GetVehicleAdsByCarrierIdQuery request, CancellationToken cancellationToken)
        {
            var vehicleAds = await _vehicleAdService.GetByCarrierIdAsync(request.CarrierId);

            // ✅ Status filtresi uygulanıyor
            if (request.Status.HasValue)
            {
                vehicleAds = vehicleAds.Where(x => x.Status == request.Status.Value).ToList();
            }

            return vehicleAds
                .Where(x => x.Active)
                .Select(ad => new VehicleAdDto
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
                    CreatedDate = ad.CreatedDate,
                    AdDate=ad.AdDate,

                    Admin1Id = ad.Admin1Id,
                    Admin2Id = ad.Admin2Id,
                    Status = ((Domain.Enums.AdStatus)ad.Status).ToString()
                })
                .ToList();
        }
    }
}
