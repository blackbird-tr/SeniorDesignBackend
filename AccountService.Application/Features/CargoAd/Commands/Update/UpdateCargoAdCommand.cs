using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.CargoAd.Commands.Update
{
    public class UpdateCargoAdCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float? Weight { get; set; }
        public string CargoType { get; set; } 
        public decimal Price { get; set; }
        public bool IsExpired { get; set; }
        public string DropCountry { get; set; }
        public string DropCity { get; set; }
        public string PickCountry { get; set; }
        public string PickCity { get; set; }
        public string currency { get; set; }
    }

    public class UpdateCargoAdCommandHandler : IRequestHandler<UpdateCargoAdCommand, bool>
    {
        private readonly ICargoAdService _cargoAdService;

        public UpdateCargoAdCommandHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<bool> Handle(UpdateCargoAdCommand request, CancellationToken cancellationToken)
        {
            var cargoAd = await _cargoAdService.GetByIdAsync(request.Id);
            if (cargoAd == null) return false;

            cargoAd.Title = request.Title;
            cargoAd.Description = request.Description;
            cargoAd.Weight = request.Weight;
            cargoAd.CargoType = request.CargoType;
            cargoAd.currency = request.currency;
            cargoAd.DropCity = request.DropCity;
            cargoAd.DropCountry = request.DropCountry;
            cargoAd.PickCity = request.PickCity;
            cargoAd.PickCountry = request.PickCountry;
            cargoAd.Price = request.Price;
            cargoAd.IsExpired = request.IsExpired;

            await _cargoAdService.UpdateAsync(cargoAd);
            return true;
        }
    }
} 