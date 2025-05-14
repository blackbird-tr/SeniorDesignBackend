using MediatR;
using AccountService.Application.Interfaces; 
using AccountService.Application.Features.Vehicle.Queries.GetAll;

namespace AccountService.Application.Features.Vehicle.Queries.GetAvailable
{
    public class GetAvailableVehiclesQuery : IRequest<List<VehicleDto>> { }

    public class GetAvailableVehiclesQueryHandler : IRequestHandler<GetAvailableVehiclesQuery, List<VehicleDto>>
    {
        private readonly IVehicleSerivce _vehicleService;

        public GetAvailableVehiclesQueryHandler(IVehicleSerivce vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<List<VehicleDto>> Handle(GetAvailableVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.GetAvailableVehiclesAsync();

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
