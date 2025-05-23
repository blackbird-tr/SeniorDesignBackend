using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.CargoAd.Commands.Create
{
    public class CreateCargoAdCommand : IRequest<CargoAdDto>
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float? Weight { get; set; }
        public string CargoType { get; set; }
        public string DropCountry { get; set; }
        public string DropCity { get; set; }
        public string PickCountry { get; set; }
        public string PickCity { get; set; }
        public string currency  { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateCargoAdCommandHandler : IRequestHandler<CreateCargoAdCommand, CargoAdDto>
    {
        private readonly ICargoAdService _cargoAdService;

        public CreateCargoAdCommandHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<CargoAdDto> Handle(CreateCargoAdCommand request, CancellationToken cancellationToken)
        {
            var cargoAd = new Domain.Entities.CargoAd
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                Weight = request.Weight,
                CargoType = request.CargoType,
                currency = request.currency,
                DropCity = request.DropCity,
                DropCountry = request.DropCountry,
                PickCity = request.PickCity,
                PickCountry = request.PickCountry,
                Price = request.Price,
                IsExpired = false
            };

            var createdAd = await _cargoAdService.AddAsync(cargoAd);

            return new CargoAdDto
            {
                Id = createdAd.Id,
                UserId = createdAd.UserId,
                Title = createdAd.Title,
                Description = createdAd.Description,
                Weight = createdAd.Weight,
                CargoType = createdAd.CargoType,
                DropCity = createdAd.DropCity,
                DropCountry = createdAd.DropCountry,
                PickCity = createdAd.PickCity,
                PickCountry = createdAd.PickCountry,
                currency = createdAd.currency,
                Price = createdAd.Price,
                IsExpired = createdAd.IsExpired,
                CreatedDate = createdAd.CreatedDate
            };
        }
    }
} 