using MediatR;
using Microsoft.AspNetCore.Identity;
using AccountService.Domain.Entities;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Carrier.Commands.CreateCarrier
{
    public class CreateCarrierCommand : IRequest<CreateCarrierResponse>
    {
        public string UserId { get; set; } // Auth üzerinden gelen ID
        public string LicenseNumber { get; set; }
        public bool AvailabilityStatus { get; set; }
    }

    public class CreateCarrierResponse
    {
        public int CarrierId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; } = "Carrier created successfully.";
    }

    public class CreateCarrierCommandHandler : IRequestHandler<CreateCarrierCommand, CreateCarrierResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICarrierService _carrierService;

        public CreateCarrierCommandHandler(UserManager<User> userManager, ICarrierService carrierService)
        {
            _userManager = userManager;
            _carrierService = carrierService;
        }

        public async Task<CreateCarrierResponse> Handle(CreateCarrierCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var carrier = new AccountService.Domain.Entities.Carrier
            {
                UserId = user.Id,
                LicenseNumber = request.LicenseNumber,
                AvailabilityStatus = request.AvailabilityStatus
            };

            var createdCarrier = await _carrierService.AddAsync(carrier);

            return new CreateCarrierResponse
            {
                CarrierId = createdCarrier.Id,
                UserId = user.Id
            };
        }
    }
}
