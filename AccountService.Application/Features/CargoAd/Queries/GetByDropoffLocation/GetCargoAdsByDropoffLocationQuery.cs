using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.CargoAd.Queries.GetByDropoffLocation
{
    public class GetCargoAdsByDropoffLocationQuery : IRequest<List<CargoAdDto>>
    {
        public int DropoffLocationId { get; set; }
    }

    public class GetCargoAdsByDropoffLocationQueryHandler : IRequestHandler<GetCargoAdsByDropoffLocationQuery, List<CargoAdDto>>
    {
        private readonly ICargoAdService _cargoAdService;

        public GetCargoAdsByDropoffLocationQueryHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<List<CargoAdDto>> Handle(GetCargoAdsByDropoffLocationQuery request, CancellationToken cancellationToken)
        {
            var cargoAds = await _cargoAdService.GetByDropoffLocationAsync(request.DropoffLocationId);

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