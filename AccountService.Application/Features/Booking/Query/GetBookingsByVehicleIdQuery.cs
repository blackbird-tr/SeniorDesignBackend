using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Booking.Commands.CreateBooking;

namespace AccountService.Application.Features.Booking.Queries.GetByVehicleId
{
    public class GetBookingsByVehicleIdQuery : IRequest<List<BookingDto>>
    {
        public int VehicleId { get; set; }
    }

    public class GetBookingsByVehicleIdQueryHandler : IRequestHandler<GetBookingsByVehicleIdQuery, List<BookingDto>>
    {
        private readonly IBookingService _bookingService;

        public GetBookingsByVehicleIdQueryHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<List<BookingDto>> Handle(GetBookingsByVehicleIdQuery request, CancellationToken cancellationToken)
        {
            var list = await _bookingService.GetByVehicleIdAsync(request.VehicleId);
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
