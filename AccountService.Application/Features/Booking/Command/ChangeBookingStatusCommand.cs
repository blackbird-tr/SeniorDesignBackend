using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Booking.Commands.ChangeStatus
{
    public class ChangeBookingStatusCommand : IRequest<bool>
    {
        public int BookingId { get; set; }
        public byte Status { get; set; } // Enum olarak BookingStatus
    }

    public class ChangeBookingStatusCommandHandler : IRequestHandler<ChangeBookingStatusCommand, bool>
    {
        private readonly IBookingService _bookingService;

        public ChangeBookingStatusCommandHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<bool> Handle(ChangeBookingStatusCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingService.GetByIdAsync(request.BookingId);
            if (booking == null || !booking.Active)
                return false;

            booking.Status = request.Status;
            await _bookingService.UpdateAsync(booking);

            return true;
        }
    }
}
