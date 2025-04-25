using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using MediatR;

namespace AccountService.Application 
{
    public class CreateVehicleTypeCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Desc { get; set; }
    }

    public class CreateVehicleTypeCommandHandler : IRequestHandler<CreateVehicleTypeCommand, int>
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;

        public CreateVehicleTypeCommandHandler(IVehicleTypeRepository vehicleTypeRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        public async Task<int> Handle(CreateVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            var vehicleType = new VehicleType
            {
                Name = request.Name,
                Desc = request.Desc
            };

            await _vehicleTypeRepository.AddAsync(vehicleType);
            return vehicleType.VehicleTypeId;
        }
    }
}