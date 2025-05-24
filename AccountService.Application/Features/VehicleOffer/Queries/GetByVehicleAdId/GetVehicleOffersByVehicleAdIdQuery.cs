using MediatR;
using AccountService.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AccountService.Application.Features.VehicleOffer.Queries.GetByVehicleAdId
{
    public class GetVehicleOffersByVehicleAdIdQuery : IRequest<List<VehicleOfferDto>>
    {
        public int VehicleAdId { get; set; }
    }

    public class GetVehicleOffersByVehicleAdIdQueryHandler : IRequestHandler<GetVehicleOffersByVehicleAdIdQuery, List<VehicleOfferDto>>
    {
        private readonly IVehicleOfferService _vehicleOfferService;

        public GetVehicleOffersByVehicleAdIdQueryHandler(IVehicleOfferService vehicleOfferService)
        {
            _vehicleOfferService = vehicleOfferService;
        }

        public async Task<List<VehicleOfferDto>> Handle(GetVehicleOffersByVehicleAdIdQuery request, CancellationToken cancellationToken)
        {
            var offers = await _vehicleOfferService.GetByVehicleAdIdAsync(request.VehicleAdId);

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