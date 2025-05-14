using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Booking.Commands.DeleteBooking
{
    public class DeleteBookingCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, bool>
    {
        private readonly IBookingService _bookingService;

        public DeleteBookingCommandHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<bool> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingService.GetByIdAsync(request.Id);
            if (booking == null) return false;

            booking.Active = false;
            await _bookingService.UpdateAsync(booking);
            return true;
        }
    }
}
