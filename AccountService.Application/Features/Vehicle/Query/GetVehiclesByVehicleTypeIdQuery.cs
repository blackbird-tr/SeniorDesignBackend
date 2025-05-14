using MediatR;
using AccountService.Application.Interfaces; 
using AccountService.Application.Features.Vehicle.Queries.GetAll;

namespace AccountService.Application.Features.Vehicle.Queries.GetByVehicleTypeId
{
    public class GetVehiclesByVehicleTypeIdQuery : IRequest<List<VehicleDto>>
    {
        public int VehicleTypeId { get; set; }
    }

    public class GetVehiclesByVehicleTypeIdQueryHandler : IRequestHandler<GetVehiclesByVehicleTypeIdQuery, List<VehicleDto>>
    {
        private readonly IVehicleSerivce _vehicleService;

        public GetVehiclesByVehicleTypeIdQueryHandler(IVehicleSerivce vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<List<VehicleDto>> Handle(GetVehiclesByVehicleTypeIdQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.GetByVehicleTypeIdAsync(request.VehicleTypeId);

            return vehicles.Select(vehicle => new VehicleDto
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate,
                AvailabilityStatus = vehicle.AvailabilityStatus,
                Capacity = vehicle.Capacity,
                Model = vehicle.Model,
                CarrierId = vehicle.CarrierId,
                VehicleTypeId = vehicle.VehicleTypeId
            }).ToList();
        }
    }
}
