using MediatR;
using AccountService.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AccountService.Domain.Enums;

namespace AccountService.Application.Features.CargoOffer.Queries.GetByReceiverId
{
    public class GetCargoOffersByReceiverIdQuery : IRequest<List<CargoOfferDto>>
    {
        public string ReceiverId { get; set; }
        public byte? Status { get; set; } // ✅ Status parametresi eklendi
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

            // ✅ Status filtresi uygulanıyor
            if (request.Status.HasValue)
            {
                offers = offers.Where(x => x.AdminStatus == request.Status.Value).ToList();
            }

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
                    CreatedDate = offer.CreatedDate,
                    Admin1Id = offer.Admin1Id,
                    Admin2Id = offer.Admin2Id,
                    AdminStatus = ((AdStatus)offer.AdminStatus).ToString()
                })
                .ToList();
        }
    }
}
