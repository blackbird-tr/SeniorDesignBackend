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
        public int PickupLocationId { get; set; }
        public int DropoffLocationId { get; set; }
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
                PickupLocationId = request.PickupLocationId,
                DropoffLocationId = request.DropoffLocationId,
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
                PickupLocationId = createdAd.PickupLocationId,
                DropoffLocationId = createdAd.DropoffLocationId,
                Price = createdAd.Price,
                IsExpired = createdAd.IsExpired,
                CreatedDate = createdAd.CreatedDate
            };
        }
    }
} 