using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities; 
using AccountService.Application.Features.Vehicle.Queries.GetAll;

namespace AccountService.Application.Features.Vehicle.Queries.GetByCarrierId
{
    public class GetVehiclesByCarrierIdQuery : IRequest<List<VehicleDto>>
    {
        public string CarrierId { get; set; }
    }

    public class GetVehiclesByCarrierIdQueryHandler : IRequestHandler<GetVehiclesByCarrierIdQuery, List<VehicleDto>>
    {
        private readonly IVehicleSerivce _vehicleService;

        public GetVehiclesByCarrierIdQueryHandler(IVehicleSerivce vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<List<VehicleDto>> Handle(GetVehiclesByCarrierIdQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.GetByCarrierIdAsync(request.CarrierId);

            return vehicles.Select(vehicle => new VehicleDto
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate, 
                Capacity = vehicle.Capacity,
                Model = vehicle.Model,
                CarrierId = vehicle.userId,
                VehicleType = vehicle.VehicleType,
                Title = vehicle.Title,CarrierName = vehicle.Carrier.UserName,
            }).ToList();
        }
    }
}
