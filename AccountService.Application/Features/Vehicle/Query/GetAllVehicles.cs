using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.Vehicle.Queries.GetAll
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } 
        public float Capacity { get; set; }
        public string? Model { get; set; }
        public string  Title { get; set; }
        public string CarrierId { get; set; } 

        public string? CarrierName { get; set; }        // Opsiyonel
        public string? VehicleType{ get; set; }    // Opsiyonel
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
                    Title = vehicle.Title,
                    Id = vehicle.Id,
                    LicensePlate = vehicle.LicensePlate, 
                    Capacity = vehicle.Capacity,
                    Model = vehicle.Model,
                    CarrierId = vehicle.userId,
                    VehicleType = vehicle.VehicleType,
                    CarrierName = vehicle.Carrier.UserName,
                })
                .ToList();
        }
    }
}