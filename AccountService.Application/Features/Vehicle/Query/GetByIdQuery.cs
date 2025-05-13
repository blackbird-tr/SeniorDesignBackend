using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Application.Features.Vehicle.Queries.GetAll;

namespace AccountService.Application.Features.Vehicle.Queries.GetById
{
    public class GetVehicleByIdQuery : IRequest<VehicleDto>
    {
        public int Id { get; set; }
    }

    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleDto>
    {
        private readonly IVehicleSerivce _vehicleService;

        public GetVehicleByIdQueryHandler(IVehicleSerivce vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<VehicleDto> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.GetByIdAsync(request.Id);
            if (vehicle == null) return null;

            return new VehicleDto
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
            };
        }
    }
}