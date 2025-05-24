using MediatR;
using AccountService.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AccountService.Application.Features.CargoOffer.Queries.GetByCargoAdId
{
    public class GetCargoOffersByCargoAdIdQuery : IRequest<List<CargoOfferDto>>
    {
        public int CargoAdId { get; set; }
    }

    public class GetCargoOffersByCargoAdIdQueryHandler : IRequestHandler<GetCargoOffersByCargoAdIdQuery, List<CargoOfferDto>>
    {
        private readonly ICargoOfferService _cargoOfferService;

        public GetCargoOffersByCargoAdIdQueryHandler(ICargoOfferService cargoOfferService)
        {
            _cargoOfferService = cargoOfferService;
        }

        public async Task<List<CargoOfferDto>> Handle(GetCargoOffersByCargoAdIdQuery request, CancellationToken cancellationToken)
        {
            var offers = await _cargoOfferService.GetByCargoAdIdAsync(request.CargoAdId);

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