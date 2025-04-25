using MediatR;
using System;

namespace AccountService.Application.Interfaces
{
    public class UpdateBookingCommand : IRequest
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int CarrierId { get; set; }
        public int VehicleId { get; set; }
        public int CargoId { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DropoffDate { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public bool IsFuelIncluded { get; set; }
    }

    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand>
    {
        private readonly IBookingRepository _bookingRepository;

        public UpdateBookingCommandHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Unit> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null) throw new Exception("Booking not found");

            booking.CustomerId = request.CustomerId;
            booking.CarrierId = request.CarrierId;
            booking.VehicleId = request.VehicleId;
            booking.CargoId = request.CargoId;
            booking.PickupDate = request.PickupDate;
            booking.DropoffDate = request.DropoffDate;
            booking.TotalPrice = request.TotalPrice;
            booking.Status = request.Status;
            booking.IsFuelIncluded = request.IsFuelIncluded;

            await _bookingRepository.UpdateAsync(booking);
            return Unit.Value;
        }
    }
}