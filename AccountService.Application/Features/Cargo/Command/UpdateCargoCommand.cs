using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Cargo.Commands.UpdateCargo
{
    public class UpdateCargoCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public float? Weight { get; set; }
        public string? CargoType { get; set; }
        public int PickupLocationId { get; set; }
        public int DropoffLocationId { get; set; }
        public byte Status { get; set; }
    }

    public class UpdateCargoCommandHandler : IRequestHandler<UpdateCargoCommand, bool>
    {
        private readonly ICargoService _cargoService;

        public UpdateCargoCommandHandler(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        public async Task<bool> Handle(UpdateCargoCommand request, CancellationToken cancellationToken)
        {
            var cargo = await _cargoService.GetByIdAsync(request.Id);
            if (cargo == null) return false;

            cargo.Description = request.Description;
            cargo.Weight = request.Weight;
            cargo.CargoType = request.CargoType;
            cargo.PickupLocationId = request.PickupLocationId;
            cargo.DropoffLocationId = request.DropoffLocationId;
            cargo.Status = request.Status;

            await _cargoService.UpdateAsync(cargo);
            return true;
        }
    }
}
