using MediatR;
using AccountService.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AccountService.Application.Features.VehicleOffer.Queries.GetAll
{
    public class GetAllVehicleOffersQuery : IRequest<List<VehicleOfferDto>>
    {
    }

    public class GetAllVehicleOffersQueryHandler : IRequestHandler<GetAllVehicleOffersQuery, List<VehicleOfferDto>>
    {
        private readonly IVehicleOfferService _vehicleOfferService;

        public GetAllVehicleOffersQueryHandler(IVehicleOfferService vehicleOfferService)
        {
            _vehicleOfferService = vehicleOfferService;
        }

        public async Task<List<VehicleOfferDto>> Handle(GetAllVehicleOffersQuery request, CancellationToken cancellationToken)
        {
            var offers = await _vehicleOfferService.GetAllAsync();

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