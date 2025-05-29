using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.VehicleAd.Queries.GetAll;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AccountService.Application.Features.VehicleAd.Queries.GetByCity
{
    public class GetVehicleAdsByCityQuery : IRequest<List<VehicleAdDto>>
    {
        public string City { get; set; }
        public byte? Status { get; set; } // ✅ Status parametresi eklendi
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

            // ✅ Status filtresi uygulanıyor
            if (request.Status.HasValue)
            {
                vehicleAds = vehicleAds.Where(ad => ad.Status == request.Status.Value).ToList();
            }

            return vehicleAds
                .Where(ad => ad.Active)
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
                    Admin1Id = ad.Admin1Id,
                    Admin2Id = ad.Admin2Id,
                    Status = ((Domain.Enums.AdStatus)ad.Status).ToString(),AdDate=ad.AdDate
                })
                .ToList();
        }
    }
}
