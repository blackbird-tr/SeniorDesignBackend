using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.CargoAd.Queries.GetById
{
    public class GetCargoAdByIdQuery : IRequest<CargoAdDto>
    {
        public int Id { get; set; }
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

            return new CargoAdDto
            {
                Id = cargoAd.Id,
                UserId = cargoAd.UserId,
                CustomerName = cargoAd.Customer.UserName,
                Title = cargoAd.Title,
                Description = cargoAd.Description,
                Weight = cargoAd.Weight,
                CargoType = cargoAd.CargoType,
                PickupLocationId = cargoAd.PickupLocationId,
                PickupLocationAddress = cargoAd.PickupLocation.Address,
                DropoffLocationId = cargoAd.DropoffLocationId,
                DropoffLocationAddress = cargoAd.DropoffLocation.Address,
                Price = cargoAd.Price,
                IsExpired = cargoAd.IsExpired,
                CreatedDate = cargoAd.CreatedDate
            };
        }
    }
} 