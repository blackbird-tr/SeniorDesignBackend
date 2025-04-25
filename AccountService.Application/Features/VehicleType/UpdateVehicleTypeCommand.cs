using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application
{
    public class UpdateVehicleTypeCommand : IRequest
    {
        public int VehicleTypeId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }

    public class UpdateVehicleTypeCommandHandler : IRequestHandler<UpdateVehicleTypeCommand>
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;

        public UpdateVehicleTypeCommandHandler(IVehicleTypeRepository vehicleTypeRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        public async Task<Unit> Handle(UpdateVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            var vehicleType = await _vehicleTypeRepository.GetByIdAsync(request.VehicleTypeId);
            if (vehicleType == null) throw new Exception("VehicleType not found");

            vehicleType.Name = request.Name;
            vehicleType.Desc = request.Desc;

            await _vehicleTypeRepository.UpdateAsync(vehicleType);
            return Unit.Value;
        }
    }
}