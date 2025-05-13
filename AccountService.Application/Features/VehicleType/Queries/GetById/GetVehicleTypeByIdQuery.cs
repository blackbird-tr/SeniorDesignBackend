using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Common.DTOs;

namespace AccountService.Application.Features.VehicleType.Queries.GetById
{
    public class GetVehicleTypeByIdQuery : IRequest<VehicleTypeResponse>
    {
        public int VehicleTypeId { get; set; }
    }

    public class GetVehicleTypeByIdQueryHandler : IRequestHandler<GetVehicleTypeByIdQuery, VehicleTypeResponse>
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public GetVehicleTypeByIdQueryHandler(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        public async Task<VehicleTypeResponse> Handle(GetVehicleTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicleType = await _vehicleTypeService.GetByIdAsync(request.VehicleTypeId);
            if (vehicleType == null)
                return null;

            return new VehicleTypeResponse
            {
                VehicleTypeId = vehicleType.Id,
                Name = vehicleType.Name,
                Description = vehicleType.Description
            };
        }
    }
} 