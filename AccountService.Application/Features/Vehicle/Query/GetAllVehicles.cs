using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.Vehicle.Queries.GetAll
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public bool AvailabilityStatus { get; set; }
        public float Capacity { get; set; }
        public string? Model { get; set; }

        public int CarrierId { get; set; }
        public int VehicleTypeId { get; set; }

        public string? CarrierName { get; set; }        // Opsiyonel
        public string? VehicleTypeName { get; set; }    // Opsiyonel
    }
    public class GetAllVehiclesQuery : IRequest<List<VehicleDto>> { }

    public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, List<VehicleDto>>
    {
        private readonly IVehicleSerivce _vehicleService;

        public GetAllVehiclesQueryHandler(IVehicleSerivce vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<List<VehicleDto>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.GetAllAsync();

            return vehicles
                .Where(x => x.Active)
                .Select(vehicle => new VehicleDto
                {
                    Id = vehicle.Id,
                    LicensePlate = vehicle.LicensePlate,
                    AvailabilityStatus = vehicle.AvailabilityStatus,
                    Capacity = vehicle.Capacity,
                    Model = vehicle.Model,
                    CarrierId = vehicle.CarrierId,
                    VehicleTypeId = vehicle.VehicleTypeId,
                    CarrierName = vehicle.Carrier?.User?.UserName,
                    VehicleTypeName = vehicle.VehicleType?.Name
                })
                .ToList();
        }
    }
}