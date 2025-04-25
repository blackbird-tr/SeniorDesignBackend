using AccountService.Application.Interfaces;
using MediatR;
using System;

namespace AccountService.Application
{
    public class UpdateCargoCommand : IRequest
    {
        public int CargoId { get; set; }
        public int CustomerId { get; set; }
        public string Desc { get; set; }
        public double Weight { get; set; }
        public string CargoType { get; set; }
        public string PickUpLocation { get; set; }
        public string DropOffLocation { get; set; }
        public string Status { get; set; }
    }

    public class UpdateCargoCommandHandler : IRequestHandler<UpdateCargoCommand>
    {
        private readonly ICargoRepository _cargoRepository;

        public UpdateCargoCommandHandler(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        public async Task<Unit> Handle(UpdateCargoCommand request, CancellationToken cancellationToken)
        {
            var cargo = await _cargoRepository.GetByIdAsync(request.CargoId);
            if (cargo == null) throw new Exception("Cargo not found");

            cargo.CustomerId = request.CustomerId;
            cargo.Desc = request.Desc;
            cargo.Weight = request.Weight;
            cargo.CargoType = request.CargoType;
            cargo.PickUpLocation = request.PickUpLocation;
            cargo.DropOffLocation = request.DropOffLocation;
            cargo.Status = request.Status;

            await _cargoRepository.UpdateAsync(cargo);
            return Unit.Value;
        }
    }
}