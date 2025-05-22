using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.CargoAd.Queries.GetByPickupLocation
{
    public class GetCargoAdsByPickupLocationQuery : IRequest<List<CargoAdDto>>
    {
        public int PickupLocationId { get; set; }
    }

    public class GetCargoAdsByPickupLocationQueryHandler : IRequestHandler<GetCargoAdsByPickupLocationQuery, List<CargoAdDto>>
    {
        private readonly ICargoAdService _cargoAdService;

        public GetCargoAdsByPickupLocationQueryHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<List<CargoAdDto>> Handle(GetCargoAdsByPickupLocationQuery request, CancellationToken cancellationToken)
        {
            var cargoAds = await _cargoAdService.GetByPickupLocationAsync(request.PickupLocationId);

            return cargoAds
                .Where(x => x.Active)
                .Select(ad => new CargoAdDto
                {
                    Id = ad.Id,
                    UserId = ad.UserId,
                    CustomerName = ad.Customer.UserName,
                    Title = ad.Title,
                    Description = ad.Description,
                    Weight = ad.Weight,
                    CargoType = ad.CargoType,
                    PickupLocationId = ad.PickupLocationId,
                    PickupLocationAddress = ad.PickupLocation.Address,
                    DropoffLocationId = ad.DropoffLocationId,
                    DropoffLocationAddress = ad.DropoffLocation.Address,
                    Price = ad.Price,
                    IsExpired = ad.IsExpired,
                    CreatedDate = ad.CreatedDate
                })
                .ToList();
        }
    }
} 