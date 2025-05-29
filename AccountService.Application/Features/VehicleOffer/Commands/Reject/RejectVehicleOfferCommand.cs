using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Features.VehicleOffer.Commands.Reject
{
    public class RejectVehicleOfferCommand : IRequest<VehicleOfferDto>
    {
        public int Id { get; set; }
        public string AdminId { get; set; }
    }

    public class RejectVehicleOfferCommandHandler : IRequestHandler<RejectVehicleOfferCommand, VehicleOfferDto>
    {
        private readonly IVehicleOfferService _vehicleOfferService;

        public RejectVehicleOfferCommandHandler(IVehicleOfferService vehicleOfferService)
        {
            _vehicleOfferService = vehicleOfferService;
        }

        public async Task<VehicleOfferDto> Handle(RejectVehicleOfferCommand request, CancellationToken cancellationToken)
        {
            var vehicleOffer = await _vehicleOfferService.GetByIdAsync(request.Id);
            if (vehicleOffer == null)
                throw new Exception("Araç teklifi bulunamadı");

            if (vehicleOffer.Admin1Id != "0" && vehicleOffer.Admin2Id != "0")
                throw new Exception("Daha önce karar verildi");

            if (vehicleOffer.Admin1Id == "0")
                vehicleOffer.Admin1Id = "-1";
            if (vehicleOffer.Admin2Id == "0")
                vehicleOffer.Admin2Id = "-1";

            vehicleOffer.AdminStatus = (byte)OfferStatus.Rejected;

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