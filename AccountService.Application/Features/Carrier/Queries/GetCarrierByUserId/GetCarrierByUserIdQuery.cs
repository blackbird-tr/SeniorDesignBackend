using MediatR;
using AccountService.Application.Interfaces; 
using AccountService.Application.Features.Carrier.Queries.GetAvailable;

namespace AccountService.Application.Features.Carrier.Queries.GetByUserId
{
    public class GetCarrierByUserIdQuery : IRequest<CarrierDto>
    {
        public string UserId { get; set; }
    }

    public class GetCarrierByUserIdQueryHandler : IRequestHandler<GetCarrierByUserIdQuery, CarrierDto>
    {
        private readonly ICarrierService _carrierService;

        public GetCarrierByUserIdQueryHandler(ICarrierService carrierService)
        {
            _carrierService = carrierService;
        }

        public async Task<CarrierDto> Handle(GetCarrierByUserIdQuery request, CancellationToken cancellationToken)
        {
            var carrier = await _carrierService.GetByUserIdAsync(request.UserId);
            if (carrier == null)
                return null;

            return new CarrierDto
            {
                Id = carrier.Id,
                LicenseNumber = carrier.LicenseNumber,
                AvailabilityStatus = carrier.AvailabilityStatus,
                UserId = carrier.UserId,
                UserName = carrier.User?.UserName,
                Email = carrier.User?.Email
            };
        }
    }
}
