using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Payment.Queries.GetTotalByBooking
{
    public class GetTotalPaidAmountByBookingIdQuery : IRequest<float?>
    {
        public int BookingId { get; set; }
    }

    public class GetTotalPaidAmountByBookingIdQueryHandler : IRequestHandler<GetTotalPaidAmountByBookingIdQuery, float?>
    {
        private readonly IPaymentService _paymentService;

        public GetTotalPaidAmountByBookingIdQueryHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<float?> Handle(GetTotalPaidAmountByBookingIdQuery request, CancellationToken cancellationToken)
        {
            return await _paymentService.GetTotalPaidAmountByBookingIdAsync(request.BookingId);
        }
    }
}
