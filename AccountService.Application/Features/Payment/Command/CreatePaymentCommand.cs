using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Payment.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<PaymentDto>
    {
        public int BookingId { get; set; }
        public float? Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
        public byte Status { get; set; }
    }

    public class PaymentDto
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public float? Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
        public byte Status { get; set; }
        public string StatusText => ((Domain.Enums.PaymentStatus)Status).ToString();
    }

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentDto>
    {
        private readonly IPaymentService _paymentService;

        public CreatePaymentCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<PaymentDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Domain.Entities.Payment
            {
                BookingId = request.BookingId,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                PaymentDate = request.PaymentDate ?? DateTime.UtcNow,
                Status = request.Status,
                Active = true
            };

            var created = await _paymentService.AddAsync(payment);

            return new PaymentDto
            {
                Id = created.Id,
                BookingId = created.BookingId,
                Amount = created.Amount,
                PaymentMethod = created.PaymentMethod,
                PaymentDate = created.PaymentDate,
                Status = created.Status
            };
        }
    }
}
