using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using MediatR;

namespace AccountService.Application
{
    public class CreateVehicleCommand : IRequest<int>
    {
        public int CarrierId { get; set; }
        public int VehicleTypeId { get; set; }
        public double Capacity { get; set; }
        public string LicensePlate { get; set; }
        public bool AvailabilityStatus { get; set; }
        public string Model { get; set; }
    }

    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, int>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public CreateVehicleCommandHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<int> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = new Vehicle
            {
                CarrierId = request.CarrierId,
                VehicleTypeId = request.VehicleTypeId,
                Capacity = request.Capacity,
                LicensePlate = request.LicensePlate,
                AvailabilityStatus = request.AvailabilityStatus,
                Model = request.Model
            };

            await _vehicleRepository.AddAsync(vehicle);
            return vehicle.VehicleId;
        }
    }
}