using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Payment.Queries.GetAll
{
    public class GetAllPaymentsQuery : IRequest<List<PaymentDto>> { }

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

    public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, List<PaymentDto>>
    {
        private readonly IPaymentService _paymentService;

        public GetAllPaymentsQueryHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<List<PaymentDto>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var list = await _paymentService.GetAllAsync();
            return list
                .Where(p => p.Active)
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    BookingId = p.BookingId,
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod,
                    PaymentDate = p.PaymentDate,
                    Status = p.Status
                })
                .ToList();
        }
    }
}
