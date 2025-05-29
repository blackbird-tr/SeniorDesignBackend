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
        private readonly IAdminService _adminService;

        public RejectVehicleAdCommandHandler(
            IVehicleAdService vehicleAdService,
            IAdminService adminService)
        {
            _vehicleAdService = vehicleAdService;
            _adminService = adminService;
        }

        public async Task<bool> Handle(RejectVehicleAdCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminService.ExistsAsync(request.AdminId))
                throw new Exception("Geçersiz admin ID");

            var vehicleAd = await _vehicleAdService.GetByIdAsync(request.VehicleAdId);

            if (vehicleAd == null)
                throw new Exception("Vehicle ad not found");

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