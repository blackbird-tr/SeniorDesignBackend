using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.VehicleType.Commands.UpdateVehicleType
{
    public class UpdateVehicleTypeCommand : IRequest<bool>
    {
        public int VehicleTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateVehicleTypeCommandHandler : IRequestHandler<UpdateVehicleTypeCommand, bool>
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public UpdateVehicleTypeCommandHandler(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        public async Task<bool> Handle(UpdateVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            var vehicleType = await _vehicleTypeService.GetByIdAsync(request.VehicleTypeId);
            if (vehicleType == null)
                return false;

            vehicleType.Name = request.Name;
            vehicleType.Description = request.Description;

            await _vehicleTypeService.UpdateAsync(vehicleType);
            return true;
        }
    }
} 