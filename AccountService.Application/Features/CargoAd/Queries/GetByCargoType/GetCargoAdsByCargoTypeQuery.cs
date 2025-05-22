using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.CargoAd.Queries.GetByCargoType
{
    public class GetCargoAdsByCargoTypeQuery : IRequest<List<CargoAdDto>>
    {
        public string CargoType { get; set; }
    }

    public class GetCargoAdsByCargoTypeQueryHandler : IRequestHandler<GetCargoAdsByCargoTypeQuery, List<CargoAdDto>>
    {
        private readonly ICargoAdService _cargoAdService;

        public GetCargoAdsByCargoTypeQueryHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<List<CargoAdDto>> Handle(GetCargoAdsByCargoTypeQuery request, CancellationToken cancellationToken)
        {
            var cargoAds = await _cargoAdService.GetByCargoTypeAsync(request.CargoType);

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