using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.Vehicle.Commands.CreateVehicle
{
    public class CreateVehicleCommand : IRequest<Domain.Entities.Vehicle>
    {
        public string CarrierId { get; set; }
        public string Title { get; set; }
        public string VehicleType { get; set; }
        public float Capacity { get; set; }
        public string LicensePlate { get; set; } 
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
                userId = request.CarrierId,
                VehicleType = request.VehicleType,
                Capacity = request.Capacity,
                LicensePlate = request.LicensePlate,
                Model = request.Model, 
                Title = request.Title
            };

            return await _vehicleService.AddAsync(vehicle);
        }
    }
}
