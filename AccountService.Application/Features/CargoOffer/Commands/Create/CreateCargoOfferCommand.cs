using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks; 

namespace AccountService.Application.Features.CargoOffer.Commands.Create
{
    public class CreateCargoOfferCommand : IRequest<CargoOfferDto>
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public int CargoAdId { get; set; }
        public decimal Price { get; set; }
        public string Message { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }

    public class CreateCargoOfferCommandHandler : IRequestHandler<CreateCargoOfferCommand, CargoOfferDto>
    {
        private readonly ICargoOfferService _cargoOfferService;
        private readonly ICargoAdService _cargoAdService;

        public CreateCargoOfferCommandHandler(
            ICargoOfferService cargoOfferService,
            ICargoAdService cargoAdService)
        {
            _cargoOfferService = cargoOfferService;
            _cargoAdService = cargoAdService;
        }

        public async Task<CargoOfferDto> Handle(CreateCargoOfferCommand request, CancellationToken cancellationToken)
        {
            // Basit validasyonlar
            if (string.IsNullOrEmpty(request.SenderId))
                throw new ArgumentException("Gönderen ID boş olamaz");

            if (string.IsNullOrEmpty(request.ReceiverId))
                throw new ArgumentException("Alıcı ID boş olamaz");

            if (request.Price <= 0)
                throw new ArgumentException("Fiyat 0'dan büyük olmalıdır");

            if (string.IsNullOrEmpty(request.Message))
                throw new ArgumentException("Mesaj boş olamaz");

            if (request.Message.Length > 500)
                throw new ArgumentException("Mesaj 500 karakterden uzun olamaz");

            // Kargo ilanı için fiyat kontrolü
            var cargoAd = await _cargoAdService.GetByIdAsync(request.CargoAdId);
            if (cargoAd == null)
                throw new ArgumentException("Kargo ilanı bulunamadı");

            // İlan fiyatının %20 altında veya %50 üstünde teklif verilemez
            var minPrice = cargoAd.Price * 0.8m; // %20 altı
            var maxPrice = cargoAd.Price * 1.5m; // %50 üstü

            if (request.Price < minPrice)
                throw new ArgumentException($"Teklif fiyatı ilan fiyatının en az %80'i olmalıdır. Minimum fiyat: {minPrice}");

            if (request.Price > maxPrice)
                throw new ArgumentException($"Teklif fiyatı ilan fiyatının en fazla %150'si olabilir. Maksimum fiyat: {maxPrice}");

            var offer = new Domain.Entities.CargoOffer
            {
                SenderId = request.SenderId,
                ReceiverId = request.ReceiverId,
                CargoAdId = request.CargoAdId,
                Price = request.Price,
                Message = request.Message,
                ExpiryDate = request.ExpiryDate
            };

            var createdOffer = await _cargoOfferService.AddAsync(offer);

            return new CargoOfferDto
            {
                Id = createdOffer.Id,
                SenderId = createdOffer.SenderId,
                SenderName = createdOffer.Sender.UserName,
                ReceiverId = createdOffer.ReceiverId,
                ReceiverName = createdOffer.Receiver.UserName,
                CargoAdId = createdOffer.CargoAdId,
                CargoAdTitle = createdOffer.CargoAd.Title,
                Price = createdOffer.Price,
                Message = createdOffer.Message,
                Status = createdOffer.Status.ToString(),
                ExpiryDate = createdOffer.ExpiryDate,
                CreatedDate = createdOffer.CreatedDate
            };
        }
    }
} 