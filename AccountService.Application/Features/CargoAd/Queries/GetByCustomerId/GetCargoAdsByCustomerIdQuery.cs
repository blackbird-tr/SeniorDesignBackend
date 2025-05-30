using AccountService.Application.DTOs;
using AccountService.Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Features.CargoAd.Queries.GetByCustomerId
{
    public class GetCargoAdsByCustomerIdQuery : IRequest<IEnumerable<CargoAdDto>>
    {
        public string CustomerId { get; set; }
        public byte? Status { get; set; }
    }

    public class GetCargoAdsByCustomerIdQueryHandler : IRequestHandler<GetCargoAdsByCustomerIdQuery, IEnumerable<CargoAdDto>>
    {
        private readonly ICargoAdService _cargoAdService;

        public GetCargoAdsByCustomerIdQueryHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<IEnumerable<CargoAdDto>> Handle(GetCargoAdsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var cargoAds = await _cargoAdService.GetByCustomerIdAsync(request.CustomerId);

            if (request.Status.HasValue)
            {
                cargoAds = cargoAds.Where(c => c.Status == request.Status.Value).ToList();
            }

            return cargoAds.Select(ad => new CargoAdDto
            {
                Id = ad.Id,
                UserId = ad.UserId,
                CustomerName = ad.Customer?.UserName,
                Title = ad.Title,
                Description = ad.Description,
                Weight = ad.Weight,
                CargoType = ad.CargoType,
                DropCity = ad.DropCity,
                DropCountry = ad.DropCountry,
                PickCity = ad.PickCity,
                PickCountry = ad.PickCountry,
                currency = ad.currency,
                Price = ad.Price,
                IsExpired = ad.IsExpired,
                CreatedDate = ad.CreatedDate,
                AdDate = ad.AdDate,
                Admin1Id = ad.Admin1Id,
                Admin2Id = ad.Admin2Id,
                Status = ((Domain.Enums.AdStatus)ad.Status).ToString()
            });
        }
    }
} 