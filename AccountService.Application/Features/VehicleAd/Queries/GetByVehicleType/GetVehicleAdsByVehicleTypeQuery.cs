using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.VehicleAd.Queries.GetAll;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AccountService.Application.Features.VehicleAd.Queries.GetByVehicleType
{
    public class GetVehicleAdsByVehicleTypeQuery : IRequest<List<VehicleAdDto>>
    {
        public string VehicleType { get; set; }
        public byte? Status { get; set; } // ✅ Status parametresi eklendi
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

            // ✅ Status filtresi uygulanıyor
            if (request.Status.HasValue)
            {
                vehicleAds = vehicleAds.Where(ad => ad.Status == request.Status.Value).ToList();
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
                    CreatedDate = ad.CreatedDate
                })
                .ToList();
        }
    }
}
