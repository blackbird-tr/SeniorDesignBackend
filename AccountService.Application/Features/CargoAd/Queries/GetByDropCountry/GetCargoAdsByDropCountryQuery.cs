using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.CargoAd.Queries.GetByDropCountry
{
    public class GetCargoAdsByDropCountryQuery : IRequest<List<CargoAdDto>>
    {
        public string Country { get; set; }
    }

    public class GetCargoAdsByDropCountryQueryHandler : IRequestHandler<GetCargoAdsByDropCountryQuery, List<CargoAdDto>>
    {
        private readonly ICargoAdService _cargoAdService;

        public GetCargoAdsByDropCountryQueryHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<List<CargoAdDto>> Handle(GetCargoAdsByDropCountryQuery request, CancellationToken cancellationToken)
        {
            var cargoAds = await _cargoAdService.GetByDropCountryAsync(request.Country);

            return cargoAds.Select(ad => new CargoAdDto
            {
                Id = ad.Id,
                Title = ad.Title,
                Description = ad.Description,
                DropCountry = ad.DropCountry,
                PickCountry = ad.PickCountry,
                DropCity = ad.DropCity,
                PickCity = ad.PickCity,
                UserId = ad.UserId,
                CustomerName = ad.Customer.UserName,
                CargoType = ad.CargoType,
                Weight = ad.Weight,
                CreatedDate = ad.CreatedDate
            }).ToList();
        }
    }
} 