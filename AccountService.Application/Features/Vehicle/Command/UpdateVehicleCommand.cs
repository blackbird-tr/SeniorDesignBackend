using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.Vehicle.Commands.UpdateVehicle
{
    public class UpdateVehicleCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int CarrierId { get; set; }
        public int VehicleTypeId { get; set; }
        public float Capacity { get; set; }
        public string LicensePlate { get; set; }
        public bool AvailabilityStatus { get; set; }
        public string? Model { get; set; }
    }

    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, bool>
    {
        private readonly IVehicleSerivce _vehicleService;

        public UpdateVehicleCommandHandler(IVehicleSerivce vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<bool> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.GetByIdAsync(request.Id);
            if (vehicle == null) return false;

            vehicle.CarrierId = request.CarrierId;
            vehicle.VehicleTypeId = request.VehicleTypeId;
            vehicle.Capacity = request.Capacity;
            vehicle.LicensePlate = request.LicensePlate;
            vehicle.AvailabilityStatus = request.AvailabilityStatus;
            vehicle.Model = request.Model;

            await _vehicleService.UpdateAsync(vehicle);
            return true;
        }
    }
}
