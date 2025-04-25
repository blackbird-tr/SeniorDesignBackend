using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application
{
    public class UpdateCarrierCommand : IRequest
    {
        public int CarrierId { get; set; }
        public int VehicleTypeId { get; set; }
        public string LicenseNumber { get; set; }
        public bool AvailabilityStatus { get; set; }
    }

    public class UpdateCarrierCommandHandler : IRequestHandler<UpdateCarrierCommand>
    {
        private readonly ICarrierRepository _carrierRepository;

        public UpdateCarrierCommandHandler(ICarrierRepository carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }

        public async Task<Unit> Handle(UpdateCarrierCommand request, CancellationToken cancellationToken)
        {
            var carrier = await _carrierRepository.GetByIdAsync(request.CarrierId);
            if (carrier == null) throw new Exception("Carrier not found");

            carrier.VehicleTypeId = request.VehicleTypeId;
            carrier.LicenseNumber = request.LicenseNumber;
            carrier.AvailabilityStatus = request.AvailabilityStatus;

            await _carrierRepository.UpdateAsync(carrier);
            return Unit.Value;
        }
    }
}