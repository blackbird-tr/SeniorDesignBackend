using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.VehicleAd.Queries.GetAll
{
    public class GetAllVehicleAdsQuery : IRequest<List<VehicleAdDto>>
    {
        public byte? Status { get; set; } // ✅ Status parametresi eklendi
    }

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

            // ✅ Status filtresi uygulandı
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
                    City = ad.City,
                    Country = ad.Country,
                    CarrierId = ad.userId,
                    CarrierName = ad.Carrier.UserName,
                    VehicleType = ad.VehicleType,
                    Capacity = ad.Capacity,
                    CreatedDate = ad.CreatedDate,
                    AdDate = ad.AdDate,
                    Admin1Id = ad.Admin1Id,
                    Admin2Id = ad.Admin2Id,
                    Status = ((Domain.Enums.AdStatus)ad.Status).ToString()


                })
                .ToList();
        }
    }
}
