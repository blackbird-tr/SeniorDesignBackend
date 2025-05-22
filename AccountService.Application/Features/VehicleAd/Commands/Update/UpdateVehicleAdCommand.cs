using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.VehicleAd.Commands.Update
{
    public class UpdateVehicleAdCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PickUpLocationId { get; set; }
        public string VehicleType { get; set; }
        public float Capacity { get; set; }
    }

    public class UpdateVehicleAdCommandHandler : IRequestHandler<UpdateVehicleAdCommand, bool>
    {
        private readonly IVehicleAdService _vehicleAdService;

        public UpdateVehicleAdCommandHandler(IVehicleAdService vehicleAdService)
        {
            _vehicleAdService = vehicleAdService;
        }

        public async Task<bool> Handle(UpdateVehicleAdCommand request, CancellationToken cancellationToken)
        {
            var vehicleAd = await _vehicleAdService.GetByIdAsync(request.Id);
            if (vehicleAd == null) return false;

            vehicleAd.Title = request.Title;
            vehicleAd.Desc = request.Description;
            vehicleAd.PickUpLocationId = request.PickUpLocationId;
            vehicleAd.VehicleType = request.VehicleType;
            vehicleAd.Capacity = request.Capacity;

            await _vehicleAdService.UpdateAsync(vehicleAd);
            return true;
        }
    }
} 