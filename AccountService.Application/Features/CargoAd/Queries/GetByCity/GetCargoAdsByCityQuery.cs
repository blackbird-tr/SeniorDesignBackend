using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Features.CargoAd.Queries.GetByCity
{
    public class GetCargoAdsByCityQuery : IRequest<List<CargoAdDto>>
    {
        public string City { get; set; }
        public byte? Status { get; set; }
    }

    public class GetCargoAdsByCityQueryHandler : IRequestHandler<GetCargoAdsByCityQuery, List<CargoAdDto>>
    {
        private readonly ICargoAdService _cargoAdService;

        public GetCargoAdsByCityQueryHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<List<CargoAdDto>> Handle(GetCargoAdsByCityQuery request, CancellationToken cancellationToken)
        {
            var cargoAds = await _cargoAdService.GetByPickCityAsync(request.City);

            if (request.Status.HasValue)
            {
                cargoAds = cargoAds.Where(c => c.Status == request.Status.Value).ToList();
            }

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
                CustomerName = ad.Customer?.UserName,
                CargoType = ad.CargoType,
                Weight = ad.Weight,
                currency = ad.currency,
                Price = ad.Price,
                IsExpired = ad.IsExpired,
                CreatedDate = ad.CreatedDate,
                AdDate = ad.AdDate,
                Admin1Id = ad.Admin1Id,
                Admin2Id = ad.Admin2Id,
                Status = ((Domain.Enums.AdStatus)ad.Status).ToString()
            }).ToList();
        }
    }
} 