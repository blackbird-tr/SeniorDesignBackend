using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AccountService.Application.Features.VehicleOffer.Commands.Create
{
    public class CreateVehicleOfferCommand : IRequest<VehicleOfferDto>
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public int VehicleAdId { get; set; }
        public string Message { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }

    public class CreateVehicleOfferCommandHandler : IRequestHandler<CreateVehicleOfferCommand, VehicleOfferDto>
    {
        private readonly IVehicleOfferService _vehicleOfferService;
        private readonly IVehicleAdService _vehicleAdService; 

        public CreateVehicleOfferCommandHandler(
            IVehicleOfferService vehicleOfferService,
            IVehicleAdService vehicleAdService)
        {
            _vehicleOfferService = vehicleOfferService;
            _vehicleAdService = vehicleAdService; 
        }

        public async Task<VehicleOfferDto> Handle(CreateVehicleOfferCommand request, CancellationToken cancellationToken)
        {
            // Basit validasyonlar
            if (string.IsNullOrEmpty(request.SenderId))
                throw new ArgumentException("Gönderen ID boş olamaz");

            if (string.IsNullOrEmpty(request.ReceiverId))
                throw new ArgumentException("Alıcı ID boş olamaz");

            if (string.IsNullOrEmpty(request.Message))
                throw new ArgumentException("Mesaj boş olamaz");

            if (request.Message.Length > 500)
                throw new ArgumentException("Mesaj 500 karakterden uzun olamaz");
             
            // Araç ilanı kontrolü
            var vehicleAd = await _vehicleAdService.GetByIdAsync(request.VehicleAdId);
            if (vehicleAd == null)
                throw new ArgumentException("Araç ilanı bulunamadı");

            var offer = new Domain.Entities.VehicleOffer
            {
                SenderId = request.SenderId,
                ReceiverId = request.ReceiverId,
                VehicleAdId = request.VehicleAdId,
                Message = request.Message,
                Status = Domain.Enums.OfferStatus.Pending,
                ExpiryDate = request.ExpiryDate ?? DateTime.UtcNow.AddDays(7)
            };

            var createdOffer = await _vehicleOfferService.AddAsync(offer);

            return new VehicleOfferDto
            {
                Id = createdOffer.Id,
                SenderId = createdOffer.SenderId, 
                ReceiverId = createdOffer.ReceiverId, 
                VehicleAdId = createdOffer.VehicleAdId,
                VehicleAdTitle = vehicleAd.Title,
                Message = createdOffer.Message,
                Status = createdOffer.Status.ToString(),
                ExpiryDate = createdOffer.ExpiryDate,
                CreatedDate = createdOffer.CreatedDate
            };
        }
    }
} 