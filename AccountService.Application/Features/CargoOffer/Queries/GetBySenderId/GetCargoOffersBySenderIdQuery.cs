using MediatR;
using AccountService.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AccountService.Application.Features.CargoOffer.Queries.GetBySenderId
{
    public class GetCargoOffersBySenderIdQuery : IRequest<List<CargoOfferDto>>
    {
        public string SenderId { get; set; }
    }

    public class GetCargoOffersBySenderIdQueryHandler : IRequestHandler<GetCargoOffersBySenderIdQuery, List<CargoOfferDto>>
    {
        private readonly ICargoOfferService _cargoOfferService;

        public GetCargoOffersBySenderIdQueryHandler(ICargoOfferService cargoOfferService)
        {
            _cargoOfferService = cargoOfferService;
        }

        public async Task<List<CargoOfferDto>> Handle(GetCargoOffersBySenderIdQuery request, CancellationToken cancellationToken)
        {
            var offers = await _cargoOfferService.GetBySenderIdAsync(request.SenderId);

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