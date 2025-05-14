using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Booking.Commands.CreateBooking;

namespace AccountService.Application.Features.Booking.Queries.GetByCarrierId
{
    public class GetBookingsByCarrierIdQuery : IRequest<List<BookingDto>>
    {
        public int CarrierId { get; set; }
    }

    public class GetBookingsByCarrierIdQueryHandler : IRequestHandler<GetBookingsByCarrierIdQuery, List<BookingDto>>
    {
        private readonly IBookingService _bookingService;

        public GetBookingsByCarrierIdQueryHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<List<BookingDto>> Handle(GetBookingsByCarrierIdQuery request, CancellationToken cancellationToken)
        {
            var list = await _bookingService.GetByCarrierIdAsync(request.CarrierId);
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
