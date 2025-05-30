using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Features.CargoAd.Queries.GetById
{
    public class GetCargoAdByIdQuery : IRequest<CargoAdDto>
    {
        public int Id { get; set; }
        public byte? Status { get; set; }
    }

    public class GetCargoAdByIdQueryHandler : IRequestHandler<GetCargoAdByIdQuery, CargoAdDto>
    {
        private readonly ICargoAdService _cargoAdService;

        public GetCargoAdByIdQueryHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<CargoAdDto> Handle(GetCargoAdByIdQuery request, CancellationToken cancellationToken)
        {
            var cargoAd = await _cargoAdService.GetByIdAsync(request.Id);
            if (cargoAd == null || !cargoAd.Active) return null;

            if (request.Status.HasValue && cargoAd.Status != request.Status.Value)
            {
                return null;
            }

            return new CargoAdDto
            {
                Id = cargoAd.Id,
                UserId = cargoAd.UserId,
                CustomerName = cargoAd.Customer?.UserName,
                Title = cargoAd.Title,
                Description = cargoAd.Description,
                Weight = cargoAd.Weight,
                CargoType = cargoAd.CargoType,
                DropCity = cargoAd.DropCity,
                DropCountry = cargoAd.DropCountry,
                PickCity = cargoAd.PickCity,
                PickCountry = cargoAd.PickCountry,
                currency = cargoAd.currency,
                Price = cargoAd.Price,
                IsExpired = cargoAd.IsExpired,
                CreatedDate = cargoAd.CreatedDate,
                AdDate = cargoAd.AdDate,
                Admin1Id = cargoAd.Admin1Id,
                Admin2Id = cargoAd.Admin2Id,
                Status = ((Domain.Enums.AdStatus)cargoAd.Status).ToString()
            };
        }
    }
} 