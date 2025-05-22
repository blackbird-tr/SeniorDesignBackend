using MediatR;
using AccountService.Application.Interfaces; 
using AccountService.Application.Features.Vehicle.Queries.GetAll;

namespace AccountService.Application.Features.Vehicle.Queries.GetByLicensePlate
{
    public class GetVehicleByLicensePlateQuery : IRequest<VehicleDto>
    {
        public string LicensePlate { get; set; }
    }

    public class GetVehicleByLicensePlateQueryHandler : IRequestHandler<GetVehicleByLicensePlateQuery, VehicleDto>
    {
        private readonly IVehicleSerivce _vehicleService;

        public GetVehicleByLicensePlateQueryHandler(IVehicleSerivce vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<VehicleDto> Handle(GetVehicleByLicensePlateQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.GetByLicensePlateAsync(request.LicensePlate);
            if (vehicle == null) return null;

            return new VehicleDto
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate, 
                Capacity = vehicle.Capacity,
                Model = vehicle.Model,
                CarrierId = vehicle.userId,
                VehicleType = vehicle.VehicleType,
                Title = vehicle.Title,
                
            };
        }
    }
}
