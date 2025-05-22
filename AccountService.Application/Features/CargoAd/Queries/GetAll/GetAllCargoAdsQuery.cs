using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.CargoAd.Queries.GetAll
{
    public class GetAllCargoAdsQuery : IRequest<List<CargoAdDto>> { }

    public class GetAllCargoAdsQueryHandler : IRequestHandler<GetAllCargoAdsQuery, List<CargoAdDto>>
    {
        private readonly ICargoAdService _cargoAdService;

        public GetAllCargoAdsQueryHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<List<CargoAdDto>> Handle(GetAllCargoAdsQuery request, CancellationToken cancellationToken)
        {
            var cargoAds = await _cargoAdService.GetAllAsync();

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