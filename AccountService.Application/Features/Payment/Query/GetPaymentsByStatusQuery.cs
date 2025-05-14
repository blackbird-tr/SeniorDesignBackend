using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Payment.Commands.CreatePayment;

namespace AccountService.Application.Features.Payment.Queries.GetByStatus
{
    public class GetPaymentsByStatusQuery : IRequest<List<PaymentDto>>
    {
        public byte Status { get; set; }
    }

    public class GetPaymentsByStatusQueryHandler : IRequestHandler<GetPaymentsByStatusQuery, List<PaymentDto>>
    {
        private readonly IPaymentService _paymentService;

        public GetPaymentsByStatusQueryHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<List<PaymentDto>> Handle(GetPaymentsByStatusQuery request, CancellationToken cancellationToken)
        {
            var payments = await _paymentService.GetByStatusAsync(request.Status);

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
