using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application
{
    public class UpdateVehicleCommand : IRequest
    {
        public int VehicleId { get; set; }
        public int CarrierId { get; set; }
        public int VehicleTypeId { get; set; }
        public double Capacity { get; set; }
        public string LicensePlate { get; set; }
        public bool AvailabilityStatus { get; set; }
        public string Model { get; set; }
    }

    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public UpdateVehicleCommandHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Unit> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
            if (vehicle == null) throw new Exception("Vehicle not found");

            vehicle.CarrierId = request.CarrierId;
            vehicle.VehicleTypeId = request.VehicleTypeId;
            vehicle.Capacity = request.Capacity;
            vehicle.LicensePlate = request.LicensePlate;
            vehicle.AvailabilityStatus = request.AvailabilityStatus;
            vehicle.Model = request.Model;

            await _vehicleRepository.UpdateAsync(vehicle);
            return Unit.Value;
        }
    }
}