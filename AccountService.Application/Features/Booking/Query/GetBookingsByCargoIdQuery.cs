using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Booking.Queries.GetAll;

namespace AccountService.Application.Features.Booking.Queries.GetByCargoId
{
    public class GetBookingsByCargoIdQuery : IRequest<List<BookingDto>>
    {
        public int CargoId { get; set; }
    }

    public class GetBookingsByCargoIdQueryHandler : IRequestHandler<GetBookingsByCargoIdQuery, List<BookingDto>>
    {
        private readonly IBookingService _bookingService;

        public GetBookingsByCargoIdQueryHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<List<BookingDto>> Handle(GetBookingsByCargoIdQuery request, CancellationToken cancellationToken)
        {
            var list = await _bookingService.GetByCargoIdAsync(request.CargoId);
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
