using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Enums;
using System;
using AccountService.Domain.Entities;
using AccountService.Application.Features.VehicleAd.Queries.GetAll;
using AccountService.Infrastructure.Extensions;

namespace AccountService.Application.Features.VehicleAd.Commands.Accept
{
    public class AcceptVehicleAdCommand : IRequest<VehicleAdDto>
    {
        public int VehicleAdId { get; set; }
        public string AdminId { get; set; }
    }

    public class AcceptVehicleAdCommandHandler : IRequestHandler<AcceptVehicleAdCommand, VehicleAdDto>
    {
        private readonly IVehicleAdService _vehicleAdService;
        private readonly IAdminService _adminService;
        private readonly IEmailService _emailService;

        public AcceptVehicleAdCommandHandler(
            IVehicleAdService vehicleAdService,
            IAdminService adminService,
            IEmailService emailService)
        {
            _vehicleAdService = vehicleAdService;
            _adminService = adminService;
            _emailService = emailService;
        }

        public async Task<VehicleAdDto> Handle(AcceptVehicleAdCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminService.ExistsAsync(request.AdminId))
                throw new Exception("Geçersiz admin ID");

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
                    throw new Exception("Admin already accept");
                }
                vehicleAd.Admin2Id = request.AdminId;
            }

            // Eğer her iki admin de onayladıysa
            if (vehicleAd.Admin1Id != "0" && vehicleAd.Admin2Id != "0")
            {
                vehicleAd.Status = (byte)AdStatus.Accepted;
                var body = vehicleAd.ToVehicleAdMailBody();
                _emailService.SendEmailAsync(vehicleAd.Carrier.Email, "Araç ilanı kabul edildi",
                    body).Wait();
            }

            await _vehicleAdService.UpdateAsync(vehicleAd);
            var createdAd = await _vehicleAdService.GetByIdAsync(request.VehicleAdId);
            return new VehicleAdDto
            {
                Id = createdAd.Id,
                Title = createdAd.Title,
                Description = createdAd.Desc,
                Country = createdAd.Country,
                City = createdAd.City,
                CarrierId = createdAd.userId,
                CarrierName = createdAd.Carrier?.UserName,
                VehicleType = createdAd.VehicleType,
                Capacity = createdAd.Capacity,
                CreatedDate = createdAd.CreatedDate,
                AdDate = createdAd.AdDate,
                Admin1Id = createdAd.Admin1Id,
                Admin2Id = createdAd.Admin2Id,
                Status = ((AdStatus)createdAd.Status).ToString()
            };
        }
    }
} 