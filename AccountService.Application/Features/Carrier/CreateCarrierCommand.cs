using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using MediatR;

namespace AccountService.Application
{
    public class CreateCarrierCommand : IRequest<int>
    {
        public int VehicleTypeId { get; set; }
        public string LicenseNumber { get; set; }
        public bool AvailabilityStatus { get; set; }
    }

    public class CreateCarrierCommandHandler : IRequestHandler<CreateCarrierCommand, int>
    {
        private readonly ICarrierRepository _carrierRepository;

        public CreateCarrierCommandHandler(ICarrierRepository carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }

        public async Task<int> Handle(CreateCarrierCommand request, CancellationToken cancellationToken)
        {
            var carrier = new Carrier
            {
                VehicleTypeId = request.VehicleTypeId,
                LicenseNumber = request.LicenseNumber,
                AvailabilityStatus = request.AvailabilityStatus
            };

            await _carrierRepository.AddAsync(carrier);
            return carrier.CarrierId;
        }
    }
}