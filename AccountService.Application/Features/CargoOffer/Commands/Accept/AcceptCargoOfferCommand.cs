using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Features.CargoOffer.Commands.Accept
{
    public class AcceptCargoOfferCommand : IRequest<CargoOfferDto>
    {
        public int Id { get; set; }
        public string AdminId { get; set; }
    }

    public class AcceptCargoOfferCommandHandler : IRequestHandler<AcceptCargoOfferCommand, CargoOfferDto>
    {
        private readonly ICargoOfferService _cargoOfferService;
        private readonly IAdminService _adminService;
         private readonly IEmailService _emailService;
        public AcceptCargoOfferCommandHandler(
            ICargoOfferService cargoOfferService,
            IAdminService adminService,
            IEmailService emailService)
        {
            _cargoOfferService = cargoOfferService;
            _adminService = adminService;
            _emailService = emailService;
        }

        public async Task<CargoOfferDto> Handle(AcceptCargoOfferCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminService.ExistsAsync(request.AdminId))
                throw new Exception("Geçersiz admin ID");

            var cargoOffer = await _cargoOfferService.GetByIdAsync(request.Id);
            if (cargoOffer == null)
                throw new Exception("Kargo teklifi bulunamadı");

            if (cargoOffer.Admin1Id == "0")
            {
                cargoOffer.Admin1Id = request.AdminId;
            }
            else if (cargoOffer.Admin2Id == "0")
            {
                if (cargoOffer.Admin1Id == request.AdminId)
                {
                    throw new Exception("Admin already accept");
                }
                cargoOffer.Admin2Id = request.AdminId;
                _emailService.SendEmailAsync(cargoOffer.Sender.Email, "Kargo Teklifi Onaylandı",
                    $"Kargo teklifi '{cargoOffer.CargoAdId}' için teklifiniz onaylandı.").Wait();
                cargoOffer.AdminStatus = (byte)AdStatus.Accepted;
            }

            await _cargoOfferService.UpdateAsync(cargoOffer);

            // Güncellenmiş veriyi tekrar çek
            var updatedOffer = await _cargoOfferService.GetByIdAsync(request.Id);

            return new CargoOfferDto
            {
                Id = updatedOffer.Id,
                SenderId = updatedOffer.SenderId,
                ReceiverId = updatedOffer.ReceiverId,
                CargoAdId = updatedOffer.CargoAdId,
                Price = updatedOffer.Price,
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