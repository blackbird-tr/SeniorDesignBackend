using MediatR;
using AccountService.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.Carrier.Commands.DeleteCarrier
{
    public class DeleteCarrierCommand : IRequest<bool>
    {
        public int CarrierId { get; set; }
    }

    public class DeleteCarrierCommandHandler : IRequestHandler<DeleteCarrierCommand, bool>
    {
        private readonly ICarrierService _carrierService;

        public DeleteCarrierCommandHandler(ICarrierService carrierService)
        {
            _carrierService = carrierService;
        }

        public async Task<bool> Handle(DeleteCarrierCommand request, CancellationToken cancellationToken)
        {
            var carrier = await _carrierService.GetByIdAsync(request.CarrierId);
            if (carrier == null)
                return false;

            carrier.Active = false;  
            await _carrierService.UpdateAsync(carrier);

            return true;
        }
    }
}
