using MediatR;
using AccountService.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AccountService.Application.Features.VehicleOffer.Queries.GetPending
{
    public class GetPendingVehicleOffersQuery : IRequest<List<VehicleOfferDto>>
    {
        public string UserId { get; set; }
    }

    public class GetPendingVehicleOffersQueryHandler : IRequestHandler<GetPendingVehicleOffersQuery, List<VehicleOfferDto>>
    {
        private readonly IVehicleOfferService _vehicleOfferService;

        public GetPendingVehicleOffersQueryHandler(IVehicleOfferService vehicleOfferService)
        {
            _vehicleOfferService = vehicleOfferService;
        }

        public async Task<List<VehicleOfferDto>> Handle(GetPendingVehicleOffersQuery request, CancellationToken cancellationToken)
        {
            var offers = await _vehicleOfferService.GetPendingOffersAsync(request.UserId);

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