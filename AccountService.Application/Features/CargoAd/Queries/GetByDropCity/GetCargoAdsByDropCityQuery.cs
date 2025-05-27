using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.CargoAd.Queries.GetByDropCity
{
    public class GetCargoAdsByDropCityQuery : IRequest<List<CargoAdDto>>
    {
        public string City { get; set; }
    }

    public class GetCargoAdsByDropCityQueryHandler : IRequestHandler<GetCargoAdsByDropCityQuery, List<CargoAdDto>>
    {
        private readonly ICargoAdService _cargoAdService;

        public GetCargoAdsByDropCityQueryHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<List<CargoAdDto>> Handle(GetCargoAdsByDropCityQuery request, CancellationToken cancellationToken)
        {
            var cargoAds = await _cargoAdService.GetByDropCityAsync(request.City);
            
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