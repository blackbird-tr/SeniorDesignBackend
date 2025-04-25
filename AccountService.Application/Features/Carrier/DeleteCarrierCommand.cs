using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application
{
    public class DeleteCarrierCommand : IRequest
    {
        public int CarrierId { get; set; }
    }

    public class DeleteCarrierCommandHandler : IRequestHandler<DeleteCarrierCommand>
    {
        private readonly ICarrierRepository _carrierRepository;

        public DeleteCarrierCommandHandler(ICarrierRepository carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }

        public async Task<Unit> Handle(DeleteCarrierCommand request, CancellationToken cancellationToken)
        {
            var carrier = await _carrierRepository.GetByIdAsync(request.CarrierId);
            if (carrier == null) throw new Exception("Carrier not found");

            await _carrierRepository.DeleteAsync(carrier);
            return Unit.Value;
        }
    }
}