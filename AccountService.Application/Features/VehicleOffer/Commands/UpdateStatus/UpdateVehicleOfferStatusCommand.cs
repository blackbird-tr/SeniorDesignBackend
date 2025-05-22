using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Features.VehicleOffer.Commands.UpdateStatus
{
    public class UpdateVehicleOfferStatusCommand : IRequest<bool>
    {
        public int OfferId { get; set; }
        public OfferStatus Status { get; set; }
    }

    public class UpdateVehicleOfferStatusCommandHandler : IRequestHandler<UpdateVehicleOfferStatusCommand, bool>
    {
        private readonly IVehicleOfferService _vehicleOfferService;

        public UpdateVehicleOfferStatusCommandHandler(IVehicleOfferService vehicleOfferService)
        {
            _vehicleOfferService = vehicleOfferService;
        }

        public async Task<bool> Handle(UpdateVehicleOfferStatusCommand request, CancellationToken cancellationToken)
        {
            return await _vehicleOfferService.UpdateOfferStatusAsync(request.OfferId, request.Status);
        }
    }
} 