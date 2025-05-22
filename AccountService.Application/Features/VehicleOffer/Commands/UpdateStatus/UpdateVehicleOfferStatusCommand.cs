using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.VehicleOffer.Commands.UpdateStatus
{
    public class UpdateVehicleOfferStatusCommand : IRequest<bool>
    {
        public int OfferId { get; set; }
        public OfferStatus Status { get; set; }
    }

    public class UpdateVehicleOfferStatusCommandHandler : IRequestHandler<UpdateVehicleOfferStatusCommand, bool>
    {
        private readonly IVehicleOfferService _vehicleOfferService;
        private readonly INotificationService _notificationService;

        public UpdateVehicleOfferStatusCommandHandler(
            IVehicleOfferService vehicleOfferService,
            INotificationService notificationService)
        {
            _vehicleOfferService = vehicleOfferService;
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(UpdateVehicleOfferStatusCommand request, CancellationToken cancellationToken)
        {
            var offer = await _vehicleOfferService.GetByIdAsync(request.OfferId);
            if (offer == null) return false;

            var oldStatus = offer.Status;
            var result = await _vehicleOfferService.UpdateOfferStatusAsync(request.OfferId, request.Status);

            if (result)
            {
                string title = "Araç Teklifi Durumu Güncellendi";
                string message = "";

                switch (request.Status)
                {
                    case OfferStatus.Accepted:
                        message = $"{offer.VehicleAd.Title} ilanına verdiğiniz teklif kabul edildi.";
                        await _notificationService.CreateNotificationAsync(
                            offer.SenderId,
                            title,
                            message,
                            NotificationType.VehicleOffer,
                            offer.Id
                        );
                        break;

                    case OfferStatus.Rejected:
                        message = $"{offer.VehicleAd.Title} ilanına verdiğiniz teklif reddedildi.";
                        await _notificationService.CreateNotificationAsync(
                            offer.SenderId,
                            title,
                            message,
                            NotificationType.VehicleOffer,
                            offer.Id
                        );
                        break;

                    case OfferStatus.Cancelled:
                        message = $"{offer.VehicleAd.Title} ilanına verilen teklif iptal edildi.";
                        // Hem gönderen hem alıcıya bildirim gönder
                        await _notificationService.CreateNotificationAsync(
                            offer.SenderId,
                            title,
                            message,
                            NotificationType.VehicleOffer,
                            offer.Id
                        );
                        await _notificationService.CreateNotificationAsync(
                            offer.ReceiverId,
                            title,
                            message,
                            NotificationType.VehicleOffer,
                            offer.Id
                        );
                        break;
                }
            }

            return result;
        }
    }
} 