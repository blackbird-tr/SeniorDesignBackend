using MediatR;
using Microsoft.AspNetCore.Identity;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Application.Features.Carrier.Queries.GetAvailable;

namespace AccountService.Application.Features.Carrier.Queries.GetById
{
    public class GetCarrierByIdQuery : IRequest<CarrierDto>
    {
        public int CarrierId { get; set; }
    }

    public class GetCarrierByIdQueryHandler : IRequestHandler<GetCarrierByIdQuery, CarrierDto>
    {
        private readonly ICarrierService _carrierService;
        private readonly UserManager<User> _userManager;

        public GetCarrierByIdQueryHandler(ICarrierService carrierService, UserManager<User> userManager)
        {
            _carrierService = carrierService;
            _userManager = userManager;
        }

        public async Task<CarrierDto> Handle(GetCarrierByIdQuery request, CancellationToken cancellationToken)
        {
            var carrier = await _carrierService.GetByIdAsync(request.CarrierId);
            if (carrier == null) return null;

            var user = await _userManager.FindByIdAsync(carrier.UserId);

            return new CarrierDto
            {
                Id = carrier.Id,
                LicenseNumber = carrier.LicenseNumber,
                AvailabilityStatus = carrier.AvailabilityStatus,
                UserId = carrier.UserId,
                UserName = user?.UserName,  
                Email = user?.Email
            };
        }
    }
}
