using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.CargoAd.Queries.GetByPickCity
{
    public class GetCargoAdsByPickCityQuery : IRequest<List<CargoAdDto>>
    {
        public string City { get; set; }
    }

    public class GetCargoAdsByPickCityQueryHandler : IRequestHandler<GetCargoAdsByPickCityQuery, List<CargoAdDto>>
    {
        private readonly ICargoAdService _cargoAdService;

        public GetCargoAdsByPickCityQueryHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<List<CargoAdDto>> Handle(GetCargoAdsByPickCityQuery request, CancellationToken cancellationToken)
        {
            var cargoAds = await _cargoAdService.GetByPickCityAsync(request.City);

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