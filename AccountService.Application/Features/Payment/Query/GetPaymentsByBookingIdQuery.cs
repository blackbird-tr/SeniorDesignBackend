using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Payment.Queries.GetByBookingId
{
    public class GetPaymentsByBookingIdQuery : IRequest<List<PaymentDto>>
    {
        public int BookingId { get; set; }
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

    public class GetPaymentsByBookingIdQueryHandler : IRequestHandler<GetPaymentsByBookingIdQuery, List<PaymentDto>>
    {
        private readonly IPaymentService _paymentService;

        public GetPaymentsByBookingIdQueryHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<List<PaymentDto>> Handle(GetPaymentsByBookingIdQuery request, CancellationToken cancellationToken)
        {
            var payments = await _paymentService.GetByBookingIdAsync(request.BookingId);

            return payments.Select(p => new PaymentDto
            {
                Id = p.Id,
                BookingId = p.BookingId,
                Amount = p.Amount,
                PaymentMethod = p.PaymentMethod,
                PaymentDate = p.PaymentDate,
                Status = p.Status
            }).ToList();
        }
    }
}
