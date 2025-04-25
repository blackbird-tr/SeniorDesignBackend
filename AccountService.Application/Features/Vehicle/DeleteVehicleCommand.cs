using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application
{
    public class DeleteVehicleCommand : IRequest
    {
        public int VehicleId { get; set; }
    }

    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public DeleteVehicleCommandHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Unit> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
            if (vehicle == null) throw new Exception("Vehicle not found");

            await _vehicleRepository.DeleteAsync(vehicle);
            return Unit.Value;
        }
    }
}