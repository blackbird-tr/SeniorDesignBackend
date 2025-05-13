using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Vehicle.Commands.DeleteVehicle
{
    public class DeleteVehicleCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, bool>
    {
        private readonly IVehicleSerivce _vehicleService;

        public DeleteVehicleCommandHandler(IVehicleSerivce vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<bool> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.GetByIdAsync(request.Id);
            if (vehicle == null) return false;

            vehicle.Active = false;
            await _vehicleService.UpdateAsync(vehicle);
            return true;
        }
    }
}
