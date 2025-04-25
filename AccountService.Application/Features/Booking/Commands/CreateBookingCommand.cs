using AccountService.Domain.Entities;
using MediatR;
using System;

namespace AccountService.Application.Interfaces
{
    public class CreateBookingCommand : IRequest<int>
    {
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

    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
    {
        private readonly IBookingRepository _bookingRepository;

        public CreateBookingCommandHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<int> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = new Booking
            {
                CustomerId = request.CustomerId,
                CarrierId = request.CarrierId,
                VehicleId = request.VehicleId,
                CargoId = request.CargoId,
                PickupDate = request.PickupDate,
                DropoffDate = request.DropoffDate,
                TotalPrice = request.TotalPrice,
                Status = request.Status,
                IsFuelIncluded = request.IsFuelIncluded
            };

            await _bookingRepository.AddAsync(booking);
            return booking.BookingId;
        }
    }
}