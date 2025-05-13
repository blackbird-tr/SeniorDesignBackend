using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.Vehicle.Commands.CreateVehicle
{
    public class CreateVehicleCommand : IRequest<Domain.Entities.Vehicle>
    {
        public int CarrierId { get; set; }
        public int VehicleTypeId { get; set; }
        public float Capacity { get; set; }
        public string LicensePlate { get; set; }
        public bool AvailabilityStatus { get; set; }
        public string? Model { get; set; }
    }

    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Domain.Entities.Vehicle>
    {
        private readonly IVehicleSerivce _vehicleService;

        public CreateVehicleCommandHandler(IVehicleSerivce vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<Domain.Entities.Vehicle> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = new Domain.Entities.Vehicle
            {
                CarrierId = request.CarrierId,
                VehicleTypeId = request.VehicleTypeId,
                Capacity = request.Capacity,
                LicensePlate = request.LicensePlate,
                AvailabilityStatus = request.AvailabilityStatus,
                Model = request.Model,
                Active = true
            };

            return await _vehicleService.AddAsync(vehicle);
        }
    }
}
