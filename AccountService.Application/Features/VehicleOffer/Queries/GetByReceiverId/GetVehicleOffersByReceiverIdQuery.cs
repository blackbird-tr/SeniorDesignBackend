using MediatR;
using AccountService.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AccountService.Application.Features.VehicleOffer.Queries.GetByReceiverId
{
    public class GetVehicleOffersByReceiverIdQuery : IRequest<List<VehicleOfferDto>>
    {
        public string ReceiverId { get; set; }
        public byte? Status { get; set; } // ✅ Status parametresi eklendi
    }

    public class GetVehicleOffersByReceiverIdQueryHandler : IRequestHandler<GetVehicleOffersByReceiverIdQuery, List<VehicleOfferDto>>
    {
        private readonly IVehicleOfferService _vehicleOfferService;

        public GetVehicleOffersByReceiverIdQueryHandler(IVehicleOfferService vehicleOfferService)
        {
            _vehicleOfferService = vehicleOfferService;
        }

        public async Task<List<VehicleOfferDto>> Handle(GetVehicleOffersByReceiverIdQuery request, CancellationToken cancellationToken)
        {
            var offers = await _vehicleOfferService.GetByReceiverIdAsync(request.ReceiverId);

            // ✅ Status filtresi uygulanıyor
            if (request.Status.HasValue)
            {
                offers = offers.Where(x => x.AdminStatus == request.Status.Value).ToList();
            }

            return offers
                 .Where(x => x.Active)
                 .Select(offer => new VehicleOfferDto
                 {
                     Id = offer.Id,
                     SenderId = offer.SenderId,
                     ReceiverId = offer.ReceiverId,
                     VehicleAdId = offer.VehicleAdId,
                     VehicleAdTitle = offer.VehicleAd?.Title,
                     Message = offer.Message,
                     Status = offer.Status,
                     ExpiryDate = offer.ExpiryDate,
                     CreatedDate = offer.CreatedDate,
                     Admin1Id = offer.Admin1Id,
                     Admin2Id = offer.Admin2Id,
                     AdminStatus = ((Domain.Enums.AdStatus)offer.AdminStatus).ToString()
                 })
                 .ToList();
        }
    }
}
