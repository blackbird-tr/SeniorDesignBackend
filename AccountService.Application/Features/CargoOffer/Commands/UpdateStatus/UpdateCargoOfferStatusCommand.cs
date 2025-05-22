using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Features.CargoOffer.Commands.UpdateStatus
{
    public class UpdateCargoOfferStatusCommand : IRequest<bool>
    {
        public int OfferId { get; set; }
        public OfferStatus Status { get; set; }
    }

    public class UpdateCargoOfferStatusCommandHandler : IRequestHandler<UpdateCargoOfferStatusCommand, bool>
    {
        private readonly ICargoOfferService _cargoOfferService;

        public UpdateCargoOfferStatusCommandHandler(ICargoOfferService cargoOfferService)
        {
            _cargoOfferService = cargoOfferService;
        }

        public async Task<bool> Handle(UpdateCargoOfferStatusCommand request, CancellationToken cancellationToken)
        {
            return await _cargoOfferService.UpdateOfferStatusAsync(request.OfferId, request.Status);
        }
    }
} 