using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.VehicleAd.Commands.Delete
{
    public class DeleteVehicleAdCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteVehicleAdCommandHandler : IRequestHandler<DeleteVehicleAdCommand, bool>
    {
        private readonly IVehicleAdService _vehicleAdService;

        public DeleteVehicleAdCommandHandler(IVehicleAdService vehicleAdService)
        {
            _vehicleAdService = vehicleAdService;
        }

        public async Task<bool> Handle(DeleteVehicleAdCommand request, CancellationToken cancellationToken)
        {
            var vehicleAd = await _vehicleAdService.GetByIdAsync(request.Id);
            if (vehicleAd == null) return false;

            vehicleAd.Active = false;
            await _vehicleAdService.UpdateAsync(vehicleAd);
            return true;
        }
    }
} 