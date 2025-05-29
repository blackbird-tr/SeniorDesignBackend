using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Enums;
using System;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.VehicleAd.Commands.Accept
{
    public class AcceptVehicleAdCommand : IRequest<bool>
    {
        public int VehicleAdId { get; set; }
        public string AdminId { get; set; }
    }

    public class AcceptVehicleAdCommandHandler : IRequestHandler<AcceptVehicleAdCommand, bool>
    {
        private readonly IVehicleAdService _vehicleAdService;

        public AcceptVehicleAdCommandHandler(IVehicleAdService vehicleAdService)
        {
            _vehicleAdService = vehicleAdService;
        }

        public async Task<bool> Handle(AcceptVehicleAdCommand request, CancellationToken cancellationToken)
        {
            var vehicleAd = await _vehicleAdService.GetByIdAsync(request.VehicleAdId);

            if (vehicleAd == null)
                throw new Exception("Vehicle ad not found");

          

            if (vehicleAd.Admin1Id == "0")
            {
                vehicleAd.Admin1Id = request.AdminId;
            }
            else if (vehicleAd.Admin2Id == "0")
            {

                if (vehicleAd.Admin1Id == request.AdminId)
                {
                    throw new Exception("Admin already accept  ");

                }
                vehicleAd.Admin2Id = request.AdminId;
            }

            // Eğer her iki admin de onayladıysa
            if (vehicleAd.Admin1Id != "0" && vehicleAd.Admin2Id != "0")
            {
                vehicleAd.Status = (byte)AdStatus.Accepted;
            }

            await _vehicleAdService.UpdateAsync(vehicleAd);
            return true;
        }
    }
} 