using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Common.DTOs;

namespace AccountService.Application.Features.VehicleType.Queries.GetAll
{
    public class GetAllVehicleTypesQuery : IRequest<List<VehicleTypeResponse>> { }

    public class GetAllVehicleTypesQueryHandler : IRequestHandler<GetAllVehicleTypesQuery, List<VehicleTypeResponse>>
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public GetAllVehicleTypesQueryHandler(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        public async Task<List<VehicleTypeResponse>> Handle(GetAllVehicleTypesQuery request, CancellationToken cancellationToken)
        {
            var vehicleTypes = await _vehicleTypeService.GetAllVehicleTypesAsync();
            return vehicleTypes.Select(vt => new VehicleTypeResponse
            {
                VehicleTypeId = vt.Id,
                Name = vt.Name,
                Description = vt.Description
            }).ToList();
        }
    }
} 