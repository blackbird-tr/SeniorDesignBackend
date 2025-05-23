using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.VehicleAd.Queries.GetAll;

namespace AccountService.Application.Features.VehicleAd.Queries.GetById
{
    public class GetVehicleAdByIdQuery : IRequest<VehicleAdDto>
    {
        public int Id { get; set; }
    }

    public class GetVehicleAdByIdQueryHandler : IRequestHandler<GetVehicleAdByIdQuery, VehicleAdDto>
    {
        private readonly IVehicleAdService _vehicleAdService;

        public GetVehicleAdByIdQueryHandler(IVehicleAdService vehicleAdService)
        {
            _vehicleAdService = vehicleAdService;
        }

        public async Task<VehicleAdDto> Handle(GetVehicleAdByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicleAd = await _vehicleAdService.GetByIdAsync(request.Id);
            if (vehicleAd == null || !vehicleAd.Active) return null;

            return new VehicleAdDto
            {
                Id = vehicleAd.Id,
                Title = vehicleAd.Title,
                Description = vehicleAd.Desc,
                Country = vehicleAd.Country,
                City = vehicleAd.City,
                CarrierId = vehicleAd.userId,
                CarrierName = vehicleAd.Carrier.UserName,
                VehicleType = vehicleAd.VehicleType,
                Capacity = vehicleAd.Capacity,
                CreatedDate = vehicleAd.CreatedDate
            };
        }
    }
} 