using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Payment.Commands.ChangeStatus
{
    public class ChangePaymentStatusCommand : IRequest<bool>
    {
        public int PaymentId { get; set; }
        public byte Status { get; set; } // Enum olarak PaymentStatus
    }

    public class ChangePaymentStatusCommandHandler : IRequestHandler<ChangePaymentStatusCommand, bool>
    {
        private readonly IPaymentService _paymentService;

        public ChangePaymentStatusCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<bool> Handle(ChangePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentService.GetByIdAsync(request.PaymentId);
            if (payment == null || !payment.Active)
                return false;

            payment.Status = request.Status;
            await _paymentService.UpdateAsync(payment);
            return true;
        }
    }
}
