using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Booking.Queries.GetAll
{
    public class GetAllBookingsQuery : IRequest<List<BookingDto>> { }

    public class BookingDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CarrierId { get; set; }
        public int VehicleId { get; set; }
        public int CargoId { get; set; }
        public float? TotalPrice { get; set; }
        public byte Status { get; set; }
        public string StatusText => ((Domain.Enums.BookingStatus)Status).ToString();
    }

    public class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, List<BookingDto>>
    {
        private readonly IBookingService _bookingService;

        public GetAllBookingsQueryHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<List<BookingDto>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _bookingService.GetAllAsync();
            return bookings
                .Where(b => b.Active)
                .Select(b => new BookingDto
                {
                    Id = b.Id,
                    CustomerId = b.CustomerId,
                    CarrierId = b.CarrierId,
                    VehicleId = b.VehicleId,
                    CargoId = b.CargoId,
                    TotalPrice = b.TotalPrice,
                    Status = b.Status
                }).ToList();
        }
    }
}
