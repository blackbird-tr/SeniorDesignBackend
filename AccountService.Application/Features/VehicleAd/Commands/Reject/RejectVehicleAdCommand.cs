using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Enums;
using System;

namespace AccountService.Application.Features.VehicleAd.Commands.Reject
{
    public class RejectVehicleAdCommand : IRequest<bool>
    {
        public int VehicleAdId { get; set; }
        public string AdminId { get; set; }
    }

    public class RejectVehicleAdCommandHandler : IRequestHandler<RejectVehicleAdCommand, bool>
    {
        private readonly IVehicleAdService _vehicleAdService;

        public RejectVehicleAdCommandHandler(IVehicleAdService vehicleAdService)
        {
            _vehicleAdService = vehicleAdService;
        }

        public async Task<bool> Handle(RejectVehicleAdCommand request, CancellationToken cancellationToken)
        {
            var vehicleAd = await _vehicleAdService.GetByIdAsync(request.VehicleAdId);

            if (vehicleAd == null)
                throw new Exception("Vehicle ad not found");

            if (vehicleAd.Admin1Id != "0" && vehicleAd.Admin2Id != "0")
                throw new Exception("Daha önce karar verildi");

            // Admin ID'sini -1 olarak set et
            if (vehicleAd.Admin1Id == "0")
            {
                vehicleAd.Admin1Id = "-1";
            }
            else if (vehicleAd.Admin2Id == "0")
            {
                vehicleAd.Admin2Id = "-1";
            }

            // Status'u Rejected olarak set et
            vehicleAd.Status = (byte)AdStatus.Rejected;

            await _vehicleAdService.UpdateAsync(vehicleAd);
            return true;
        }
    }
} 