using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Payment.Commands.UpdatePayment
{
    public class UpdatePaymentCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public float? Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
        public byte Status { get; set; }
    }

    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, bool>
    {
        private readonly IPaymentService _paymentService;

        public UpdatePaymentCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<bool> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentService.GetByIdAsync(request.Id);
            if (payment == null || !payment.Active)
                return false;

            payment.Amount = request.Amount;
            payment.PaymentMethod = request.PaymentMethod;
            payment.PaymentDate = request.PaymentDate;
            payment.Status = request.Status;

            await _paymentService.UpdateAsync(payment);
            return true;
        }
    }
}
