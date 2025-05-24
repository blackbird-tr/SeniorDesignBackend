using MediatR;
using AccountService.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Features.CargoOffer.Queries.GetById
{
    public class GetCargoOfferByIdQuery : IRequest<CargoOfferDto>
    {
        public int Id { get; set; }
    }

    public class GetCargoOfferByIdQueryHandler : IRequestHandler<GetCargoOfferByIdQuery, CargoOfferDto>
    {
        private readonly ICargoOfferService _cargoOfferService;

        public GetCargoOfferByIdQueryHandler(ICargoOfferService cargoOfferService)
        {
            _cargoOfferService = cargoOfferService;
        }

        public async Task<CargoOfferDto> Handle(GetCargoOfferByIdQuery request, CancellationToken cancellationToken)
        {
            var offer = await _cargoOfferService.GetByIdAsync(request.Id);

            if (offer == null || !offer.Active)
                return null;

            return new CargoOfferDto
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
            };
        }
    }
} 