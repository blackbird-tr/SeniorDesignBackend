using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.CargoAd.Queries.GetByPickCountry
{
    public class GetCargoAdsByPickCountryQuery : IRequest<List<CargoAdDto>>
    {
        public string Country { get; set; }
    }

    public class GetCargoAdsByPickCountryQueryHandler : IRequestHandler<GetCargoAdsByPickCountryQuery, List<CargoAdDto>>
    {
        private readonly ICargoAdService _cargoAdService;

        public GetCargoAdsByPickCountryQueryHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<List<CargoAdDto>> Handle(GetCargoAdsByPickCountryQuery request, CancellationToken cancellationToken)
        {
            var cargoAds = await _cargoAdService.GetByPickCountryAsync(request.Country);
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