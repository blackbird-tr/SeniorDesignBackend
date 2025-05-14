using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Booking.Commands.UpdateBooking
{
    public class UpdateBookingCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DropoffDate { get; set; }
        public float? TotalPrice { get; set; }
        public bool IsFuelIncluded { get; set; }
        public byte Status { get; set; }
    }

    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, bool>
    {
        private readonly IBookingService _bookingService;

        public UpdateBookingCommandHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<bool> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingService.GetByIdAsync(request.Id);
            if (booking == null) return false;

            booking.PickupDate = request.PickupDate;
            booking.DropoffDate = request.DropoffDate;
            booking.TotalPrice = request.TotalPrice;
            booking.IsFuelIncluded = request.IsFuelIncluded;
            booking.Status = request.Status;

            await _bookingService.UpdateAsync(booking);
            return true;
        }
    }
}
