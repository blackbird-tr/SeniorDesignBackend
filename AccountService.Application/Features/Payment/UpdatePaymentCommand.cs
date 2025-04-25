using AccountService.Application.Interfaces;
using MediatR;
using System;

namespace AccountService.Application
{
    public class UpdatePaymentCommand : IRequest
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
    }

    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand>
    {
        private readonly IPaymentRepository _paymentRepository;

        public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Unit> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);
            if (payment == null) throw new Exception("Payment not found");

            payment.BookingId = request.BookingId;
            payment.Amount = request.Amount;
            payment.PaymentMethod = request.PaymentMethod;
            payment.PaymentDate = request.PaymentDate;
            payment.Status = request.Status;

            await _paymentRepository.UpdateAsync(payment);
            return Unit.Value;
        }
    }
}