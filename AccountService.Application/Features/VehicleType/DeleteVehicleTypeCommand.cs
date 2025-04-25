using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application
{
    public class DeleteVehicleTypeCommand : IRequest
    {
        public int VehicleTypeId { get; set; }
    }

    public class DeleteVehicleTypeCommandHandler : IRequestHandler<DeleteVehicleTypeCommand>
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;

        public DeleteVehicleTypeCommandHandler(IVehicleTypeRepository vehicleTypeRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        public async Task<Unit> Handle(DeleteVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            var vehicleType = await _vehicleTypeRepository.GetByIdAsync(request.VehicleTypeId);
            if (vehicleType == null) throw new Exception("VehicleType not found");

            await _vehicleTypeRepository.DeleteAsync(vehicleType);
            return Unit.Value;
        }
    }
}