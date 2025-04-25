using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using MediatR;
using System;

namespace AccountService.Application
{
    public class CreatePaymentCommand : IRequest<int>
    {
        public int BookingId { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
    }

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, int>
    {
        private readonly IPaymentRepository _paymentRepository;

        public CreatePaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                BookingId = request.BookingId,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                PaymentDate = request.PaymentDate,
                Status = request.Status
            };

            await _paymentRepository.AddAsync(payment);
            return payment.PaymentId;
        }
    }
}