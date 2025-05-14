using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Payment.Queries.GetAll;

namespace AccountService.Application.Features.Payment.Queries.GetByDateRange
{
    public class GetPaymentsByDateRangeQuery : IRequest<List<PaymentDto>>
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class GetPaymentsByDateRangeQueryHandler : IRequestHandler<GetPaymentsByDateRangeQuery, List<PaymentDto>>
    {
        private readonly IPaymentService _paymentService;

        public GetPaymentsByDateRangeQueryHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<List<PaymentDto>> Handle(GetPaymentsByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var payments = await _paymentService.GetByDateRangeAsync(request.Start, request.End);

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
