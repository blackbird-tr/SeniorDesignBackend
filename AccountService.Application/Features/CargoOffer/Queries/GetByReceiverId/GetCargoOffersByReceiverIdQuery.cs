using MediatR;
using AccountService.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AccountService.Application.Features.CargoOffer.Queries.GetByReceiverId
{
    public class GetCargoOffersByReceiverIdQuery : IRequest<List<CargoOfferDto>>
    {
        public string ReceiverId { get; set; }
    }

    public class GetCargoOffersByReceiverIdQueryHandler : IRequestHandler<GetCargoOffersByReceiverIdQuery, List<CargoOfferDto>>
    {
        private readonly ICargoOfferService _cargoOfferService;

        public GetCargoOffersByReceiverIdQueryHandler(ICargoOfferService cargoOfferService)
        {
            _cargoOfferService = cargoOfferService;
        }

        public async Task<List<CargoOfferDto>> Handle(GetCargoOffersByReceiverIdQuery request, CancellationToken cancellationToken)
        {
            var offers = await _cargoOfferService.GetByReceiverIdAsync(request.ReceiverId);

            return offers
                .Where(x => x.Active)
                .Select(offer => new CargoOfferDto
                {
                    Id = offer.Id,
                    SenderId = offer.SenderId,
                    ReceiverId = offer.ReceiverId,
                    CargoAdId = offer.CargoAdId,
                    CargoAdTitle = offer.CargoAd?.Title,
                    Price = offer.Price,
                    Message = offer.Message,
                    Status = offer.Status.ToString(),
                    ExpiryDate = offer.ExpiryDate,
                    CreatedDate = offer.CreatedDate
                })
                .ToList();
        }
    }
} 