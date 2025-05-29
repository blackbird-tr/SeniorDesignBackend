using MediatR;
using AccountService.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AccountService.Application.Features.VehicleOffer.Queries.GetBySenderId
{
    public class GetVehicleOffersBySenderIdQuery : IRequest<List<VehicleOfferDto>>
    {
        public string SenderId { get; set; }
        public byte? Status { get; set; } // ✅ Status parametresi eklendi
    }

    public class GetVehicleOffersBySenderIdQueryHandler : IRequestHandler<GetVehicleOffersBySenderIdQuery, List<VehicleOfferDto>>
    {
        private readonly IVehicleOfferService _vehicleOfferService;

        public GetVehicleOffersBySenderIdQueryHandler(IVehicleOfferService vehicleOfferService)
        {
            _vehicleOfferService = vehicleOfferService;
        }

        public async Task<List<VehicleOfferDto>> Handle(GetVehicleOffersBySenderIdQuery request, CancellationToken cancellationToken)
        {
            var offers = await _vehicleOfferService.GetBySenderIdAsync(request.SenderId);

            // ✅ Status filtresi uygulandı
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
                    CreatedDate = offer.CreatedDate
                })
                .ToList();
        }
    }
}
