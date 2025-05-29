using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.CargoOffer.Commands.UpdateStatus
{
    public class UpdateCargoOfferStatusCommand : IRequest<bool>
    {
        public int OfferId { get; set; }
        public string Status { get; set; }
    }

    public class UpdateCargoOfferStatusCommandHandler : IRequestHandler<UpdateCargoOfferStatusCommand, bool>
    {
        private readonly ICargoOfferService _cargoOfferService;
        private readonly INotificationService _notificationService;

        public UpdateCargoOfferStatusCommandHandler(
            ICargoOfferService cargoOfferService,
            INotificationService notificationService)
        {
            _cargoOfferService = cargoOfferService;
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(UpdateCargoOfferStatusCommand request, CancellationToken cancellationToken)
        {
            var offer = await _cargoOfferService.GetByIdAsync(request.OfferId);
            if (offer == null) return false;

            var oldStatus = offer.Status;
            var result = await _cargoOfferService.UpdateOfferStatusAsync(request.OfferId, request.Status);

            if (result)
            {
                string title = "Kargo Teklifi Durumu Güncellendi";
                string message = "";

                switch (request.Status)
                {
                    case "Accepted":
                        message = $"{offer.CargoAd.Title} ilanına verdiğiniz teklif kabul edildi.";
                        await _notificationService.CreateNotificationAsync(
                            offer.ReceiverId,
                            title,
                            message,
                            NotificationType.CargoOffer,
                            offer.Id
                        );
                        break;

                    case "Rejected":
                        message = $"{offer.CargoAd.Title} ilanına verdiğiniz teklif reddedildi.";
                        await _notificationService.CreateNotificationAsync(
                            offer.ReceiverId,
                            title,
                            message,
                            NotificationType.CargoOffer,
                            offer.Id
                        );
                        break;

                    case "Cancelled":
                        message = $"{offer.CargoAd.Title} ilanına verilen teklif iptal edildi.";
                        // Hem gönderen hem alıcıya bildirim gönder
                        await _notificationService.CreateNotificationAsync(
                            offer.ReceiverId,
                            title,
                            message,
                            NotificationType.CargoOffer,
                            offer.Id
                        );
                        await _notificationService.CreateNotificationAsync(
                            offer.ReceiverId,
                            title,
                            message,
                            NotificationType.CargoOffer,
                            offer.Id
                        );
                        break;
                }
            }

            return result;
        }
    }
} 