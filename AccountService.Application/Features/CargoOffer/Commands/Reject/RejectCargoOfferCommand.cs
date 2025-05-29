using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Features.CargoOffer.Commands.Reject
{
    public class RejectCargoOfferCommand : IRequest<CargoOfferDto>
    {
        public int Id { get; set; }
        public string AdminId { get; set; }
    }

    public class RejectCargoOfferCommandHandler : IRequestHandler<RejectCargoOfferCommand, CargoOfferDto>
    {
        private readonly ICargoOfferService _cargoOfferService;
        private readonly IAdminService _adminService;

        public RejectCargoOfferCommandHandler(
            ICargoOfferService cargoOfferService,
            IAdminService adminService)
        {
            _cargoOfferService = cargoOfferService;
            _adminService = adminService;
        }

        public async Task<CargoOfferDto> Handle(RejectCargoOfferCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminService.ExistsAsync(request.AdminId))
                throw new Exception("Geçersiz admin ID");

            var cargoOffer = await _cargoOfferService.GetByIdAsync(request.Id);
            if (cargoOffer == null)
                throw new Exception("Kargo teklifi bulunamadı");

            if (cargoOffer.Admin1Id == "0")
                cargoOffer.Admin1Id = "-1";
            if (cargoOffer.Admin2Id == "0")
                cargoOffer.Admin2Id = "-1";

            cargoOffer.AdminStatus = (byte)OfferStatus.Rejected;

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