using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.VehicleType.Commands.CreateVehicleType
{
    public class CreateVehicleTypeCommand : IRequest<CreateVehicleTypeResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateVehicleTypeResponse
    {
        public int VehicleTypeId { get; set; }
        public string Message { get; set; } = "Vehicle type created successfully.";
    }

    public class CreateVehicleTypeCommandHandler : IRequestHandler<CreateVehicleTypeCommand, CreateVehicleTypeResponse>
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public CreateVehicleTypeCommandHandler(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        public async Task<CreateVehicleTypeResponse> Handle(CreateVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            var vehicleType = new AccountService.Domain.Entities.VehicleType
            {
                Name = request.Name,
                Description = request.Description
            };

            var createdVehicleType = await _vehicleTypeService.AddAsync(vehicleType);

            return new CreateVehicleTypeResponse
            {
                VehicleTypeId = createdVehicleType.Id
            };
        }
    }
} 