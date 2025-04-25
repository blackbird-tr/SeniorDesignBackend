using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application
{
    public class DeletePaymentCommand : IRequest
    {
        public int PaymentId { get; set; }
    }

    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand>
    {
        private readonly IPaymentRepository _paymentRepository;

        public DeletePaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Unit> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);
            if (payment == null) throw new Exception("Payment not found");

            await _paymentRepository.DeleteAsync(payment);
            return Unit.Value;
        }
    }
}