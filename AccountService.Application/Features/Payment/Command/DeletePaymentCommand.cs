using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Payment.Commands.DeletePayment
{
    public class DeletePaymentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, bool>
    {
        private readonly IPaymentService _paymentService;

        public DeletePaymentCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<bool> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentService.GetByIdAsync(request.Id);
            if (payment == null) return false;

            payment.Active = false;
            await _paymentService.UpdateAsync(payment);
            return true;
        }
    }
}
