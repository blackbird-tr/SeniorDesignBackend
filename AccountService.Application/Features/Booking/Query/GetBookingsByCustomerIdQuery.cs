using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Booking.Queries.GetByCustomerId
{
    public class GetBookingsByCustomerIdQuery : IRequest<List<BookingDto>>
    {
        public int CustomerId { get; set; }
    }

    public class BookingDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public float? TotalPrice { get; set; }
        public byte Status { get; set; }
        public string StatusText => ((Domain.Enums.BookingStatus)Status).ToString();
    }

    public class GetBookingsByCustomerIdQueryHandler : IRequestHandler<GetBookingsByCustomerIdQuery, List<BookingDto>>
    {
        private readonly IBookingService _bookingService;

        public GetBookingsByCustomerIdQueryHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<List<BookingDto>> Handle(GetBookingsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var list = await _bookingService.GetByCustomerIdAsync(request.CustomerId);
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
