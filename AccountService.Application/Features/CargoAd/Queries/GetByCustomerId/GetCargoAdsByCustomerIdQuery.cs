using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.CargoAd.Queries.GetByCustomerId
{
    public class GetCargoAdsByCustomerIdQuery : IRequest<List<CargoAdDto>>
    {
        public string CustomerId { get; set; }
    }

    public class GetCargoAdsByCustomerIdQueryHandler : IRequestHandler<GetCargoAdsByCustomerIdQuery, List<CargoAdDto>>
    {
        private readonly ICargoAdService _cargoAdService;

        public GetCargoAdsByCustomerIdQueryHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<List<CargoAdDto>> Handle(GetCargoAdsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var cargoAds = await _cargoAdService.GetByCustomerIdAsync(request.CustomerId);

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