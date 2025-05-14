using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Booking.Commands.CreateBooking;

namespace AccountService.Application.Features.Booking.Queries.GetByStatus
{
    public class GetBookingsByStatusQuery : IRequest<List<BookingDto>>
    {
        public byte Status { get; set; }
    }

    public class GetBookingsByStatusQueryHandler : IRequestHandler<GetBookingsByStatusQuery, List<BookingDto>>
    {
        private readonly IBookingService _bookingService;

        public GetBookingsByStatusQueryHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<List<BookingDto>> Handle(GetBookingsByStatusQuery request, CancellationToken cancellationToken)
        {
            var list = await _bookingService.GetByStatusAsync(request.Status);
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
