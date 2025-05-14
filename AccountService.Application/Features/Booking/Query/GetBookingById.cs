using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Booking.Queries.GetById
{
    public class GetBookingByIdQuery : IRequest<BookingDto>
    {
        public int Id { get; set; }
    }

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

    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDto>
    {
        private readonly IBookingService _bookingService;

        public GetBookingByIdQueryHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<BookingDto?> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var booking = await _bookingService.GetByIdAsync(request.Id);
            if (booking == null || !booking.Active) return null;

            return new BookingDto
            {
                Id = booking.Id,
                CustomerId = booking.CustomerId,
                CarrierId = booking.CarrierId,
                VehicleId = booking.VehicleId,
                CargoId = booking.CargoId,
                TotalPrice = booking.TotalPrice,
                Status = booking.Status
            };
        }
    }
}
