using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Booking.Commands.CreateBooking;

namespace AccountService.Application.Features.Booking.Queries.GetByPickupDateRange
{
    public class GetBookingsByPickupDateRangeQuery : IRequest<List<BookingDto>>
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class GetBookingsByPickupDateRangeQueryHandler : IRequestHandler<GetBookingsByPickupDateRangeQuery, List<BookingDto>>
    {
        private readonly IBookingService _bookingService;

        public GetBookingsByPickupDateRangeQueryHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<List<BookingDto>> Handle(GetBookingsByPickupDateRangeQuery request, CancellationToken cancellationToken)
        {
            var list = await _bookingService.GetByPickupDateRangeAsync(request.Start, request.End);
            return list.Select(b => new BookingDto
            {
                Id = b.Id,
                CustomerId = b.CustomerId,
                TotalPrice = b.TotalPrice,
                Status = b.Status
            }).ToList();
        }
    }
}
