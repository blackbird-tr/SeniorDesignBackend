using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.Carrier.Commands.UpdateCarrier
{
    public class UpdateCarrierCommand : IRequest<bool>
    {
        public int CarrierId { get; set; }
        public string LicenseNumber { get; set; }
        public bool AvailabilityStatus { get; set; }
    }

    public class UpdateCarrierCommandHandler : IRequestHandler<UpdateCarrierCommand, bool>
    {
        private readonly ICarrierService _carrierService;

        public UpdateCarrierCommandHandler(ICarrierService carrierService)
        {
            _carrierService = carrierService;
        }

        public async Task<bool> Handle(UpdateCarrierCommand request, CancellationToken cancellationToken)
        {
            var carrier = await _carrierService.GetByIdAsync(request.CarrierId);
            if (carrier == null)
                return false;

            carrier.LicenseNumber = request.LicenseNumber;
            carrier.AvailabilityStatus = request.AvailabilityStatus;

            await _carrierService.UpdateAsync(carrier);
            return true;
        }
    }
}
