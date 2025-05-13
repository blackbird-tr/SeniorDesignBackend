using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.VehicleType.Commands.DeleteVehicleType
{
    public class DeleteVehicleTypeCommand : IRequest<bool>
    {
        public int VehicleTypeId { get; set; }
    }

    public class DeleteVehicleTypeCommandHandler : IRequestHandler<DeleteVehicleTypeCommand, bool>
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public DeleteVehicleTypeCommandHandler(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        public async Task<bool> Handle(DeleteVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            var vehicleType = await _vehicleTypeService.GetByIdAsync(request.VehicleTypeId);
            if (vehicleType == null)
                return false;

            vehicleType.Active = false;
            await _vehicleTypeService.UpdateAsync(vehicleType);

            return true;
        }
    }
} 