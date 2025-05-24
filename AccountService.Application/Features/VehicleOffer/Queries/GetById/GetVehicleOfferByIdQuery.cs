using MediatR;
using AccountService.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Features.VehicleOffer.Queries.GetById
{
    public class GetVehicleOfferByIdQuery : IRequest<VehicleOfferDto>
    {
        public int Id { get; set; }
    }

    public class GetVehicleOfferByIdQueryHandler : IRequestHandler<GetVehicleOfferByIdQuery, VehicleOfferDto>
    {
        private readonly IVehicleOfferService _vehicleOfferService;

        public GetVehicleOfferByIdQueryHandler(IVehicleOfferService vehicleOfferService)
        {
            _vehicleOfferService = vehicleOfferService;
        }

        public async Task<VehicleOfferDto> Handle(GetVehicleOfferByIdQuery request, CancellationToken cancellationToken)
        {
            var offer = await _vehicleOfferService.GetByIdAsync(request.Id);

            if (offer == null || !offer.Active)
                return null;

            return new VehicleOfferDto
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
            };
        }
    }
} 