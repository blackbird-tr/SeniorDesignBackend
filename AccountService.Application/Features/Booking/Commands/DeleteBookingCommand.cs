using MediatR;

namespace AccountService.Application.Interfaces
{
    public class DeleteBookingCommand : IRequest
    {
        public int BookingId { get; set; }
    }

    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
    {
        private readonly IBookingRepository _bookingRepository;

        public DeleteBookingCommandHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null) throw new Exception("Booking not found");

            await _bookingRepository.DeleteAsync(booking);
            return Unit.Value;
        }
    }
}