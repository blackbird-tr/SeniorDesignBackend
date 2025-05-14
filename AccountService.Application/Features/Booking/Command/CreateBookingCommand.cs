using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Booking.Commands.CreateBooking
{
    public class CreateBookingCommand : IRequest<BookingDto>
    {
        public int CustomerId { get; set; }
        public int CarrierId { get; set; }
        public int VehicleId { get; set; }
        public int CargoId { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DropoffDate { get; set; }
        public float? TotalPrice { get; set; }
        public bool IsFuelIncluded { get; set; }
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

    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
    {
        private readonly IBookingService _bookingService;

        public CreateBookingCommandHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = new Domain.Entities.Booking
            {
                CustomerId = request.CustomerId,
                CarrierId = request.CarrierId,
                VehicleId = request.VehicleId,
                CargoId = request.CargoId,
                PickupDate = request.PickupDate,
                DropoffDate = request.DropoffDate,
                TotalPrice = request.TotalPrice,
                IsFuelIncluded = request.IsFuelIncluded,
                Status = (byte)Domain.Enums.BookingStatus.Pending,
                Active = true
            };

            var created = await _bookingService.AddAsync(booking);

            return new BookingDto
            {
                Id = created.Id,
                CustomerId = created.CustomerId,
                CarrierId = created.CarrierId,
                VehicleId = created.VehicleId,
                CargoId = created.CargoId,
                TotalPrice = created.TotalPrice,
                Status = created.Status
            };
        }
    }
}
