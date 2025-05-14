using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.Cargo.Commands.CreateCargo
{
    public class CreateCargoCommand : IRequest<CargoDto>
    {
        public int CustomerId { get; set; }
        public string? Description { get; set; }
        public float? Weight { get; set; }
        public string? CargoType { get; set; }
        public int PickupLocationId { get; set; }
        public int DropoffLocationId { get; set; }
    }

    public class CargoDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public float? Weight { get; set; }
        public string? CargoType { get; set; }
        public byte Status { get; set; }
        public string StatusText => ((Domain.Enums.CargoStatus)Status).ToString();
    }

    public class CreateCargoCommandHandler : IRequestHandler<CreateCargoCommand, CargoDto>
    {
        private readonly ICargoService _cargoService;

        public CreateCargoCommandHandler(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        public async Task<CargoDto> Handle(CreateCargoCommand request, CancellationToken cancellationToken)
        {
            var cargo = new Domain.Entities.Cargo
            {
                CustomerId = request.CustomerId,
                Description = request.Description,
                Weight = request.Weight,
                CargoType = request.CargoType,
                PickupLocationId = request.PickupLocationId,
                DropoffLocationId = request.DropoffLocationId,
                Status = (byte)Domain.Enums.CargoStatus.Pending,
                Active = true
            };

            var created = await _cargoService.AddAsync(cargo);

            return new CargoDto
            {
                Id = created.Id,
                Description = created.Description,
                Weight = created.Weight,
                CargoType = created.CargoType,
                Status = created.Status
            };
        }
    }
}
