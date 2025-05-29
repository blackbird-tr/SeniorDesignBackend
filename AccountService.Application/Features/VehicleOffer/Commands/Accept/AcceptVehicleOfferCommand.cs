using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using AccountService.Infrastructure.Extensions;

namespace AccountService.Application.Features.VehicleOffer.Commands.Accept
{
    public class AcceptVehicleOfferCommand : IRequest<VehicleOfferDto>
    {
        public int Id { get; set; }
        public string AdminId { get; set; }
    }

    public class AcceptVehicleOfferCommandHandler : IRequestHandler<AcceptVehicleOfferCommand, VehicleOfferDto>
    {
        private readonly IVehicleOfferService _vehicleOfferService;
        private readonly IAdminService _adminService;
        private readonly IEmailService _emailService;
        public AcceptVehicleOfferCommandHandler(
            IVehicleOfferService vehicleOfferService,
            IAdminService adminService,
            IEmailService emailService)
        {
            _vehicleOfferService = vehicleOfferService;
            _adminService = adminService;
            _emailService = emailService;
        }

        public async Task<VehicleOfferDto> Handle(AcceptVehicleOfferCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminService.ExistsAsync(request.AdminId))
                throw new Exception("Geçersiz admin ID");

            var vehicleOffer = await _vehicleOfferService.GetByIdAsync(request.Id);
            if (vehicleOffer == null)
                throw new Exception("Araç teklifi bulunamadı");

            if (vehicleOffer.Admin1Id == "0")
            {
                vehicleOffer.Admin1Id = request.AdminId;
            }
            else if (vehicleOffer.Admin2Id == "0")
            {
                if (vehicleOffer.Admin1Id == request.AdminId)
                {
                    throw new Exception("Admin already accept");
                }
                vehicleOffer.Admin2Id = request.AdminId;
                vehicleOffer.AdminStatus = (byte)OfferStatus.Accepted;
                var body = vehicleOffer.ToVehicleOfferMailBody();
                _emailService.SendEmailAsync(vehicleOffer.Sender.Email, "Araç Teklifi Onaylandı",
                    body).Wait();
            }

            await _vehicleOfferService.UpdateAsync(vehicleOffer);

            // Güncellenmiş veriyi tekrar çek
            var updatedOffer = await _vehicleOfferService.GetByIdAsync(request.Id);

            return new VehicleOfferDto
            {
                Id = updatedOffer.Id,
                SenderId = updatedOffer.SenderId,
                ReceiverId = updatedOffer.ReceiverId,
                VehicleAdId = updatedOffer.VehicleAdId,
                Message = updatedOffer.Message,
                Status = updatedOffer.Status,
                ExpiryDate = updatedOffer.ExpiryDate,
                CreatedDate = updatedOffer.CreatedDate,
                Admin1Id = updatedOffer.Admin1Id,
                Admin2Id = updatedOffer.Admin2Id,
                AdminStatus = ((AdStatus)updatedOffer.AdminStatus).ToString()
            };
        }
    }
} 