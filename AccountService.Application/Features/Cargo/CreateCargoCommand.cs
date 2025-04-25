using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using MediatR;
using System;

namespace AccountService.Application
{
    public class CreateCargoCommand : IRequest<int>
    {
        public int CustomerId { get; set; }
        public string Desc { get; set; }
        public double Weight { get; set; }
        public string CargoType { get; set; }
        public string PickUpLocation { get; set; }
        public string DropOffLocation { get; set; }
        public string Status { get; set; }
    }

    public class CreateCargoCommandHandler : IRequestHandler<CreateCargoCommand, int>
    {
        private readonly ICargoRepository _cargoRepository;

        public CreateCargoCommandHandler(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        public async Task<int> Handle(CreateCargoCommand request, CancellationToken cancellationToken)
        {
            var cargo = new Cargo
            {
                CustomerId = request.CustomerId,
                Desc = request.Desc,
                Weight = request.Weight,
                CargoType = request.CargoType,
                PickUpLocation = request.PickUpLocation,
                DropOffLocation = request.DropOffLocation,
                Status = request.Status
            };

            await _cargoRepository.AddAsync(cargo);
            return cargo.CargoId;
        }
    }
}