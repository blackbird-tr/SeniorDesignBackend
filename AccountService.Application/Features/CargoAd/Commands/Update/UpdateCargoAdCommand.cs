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
        public int PickupLocationId { get; set; }
        public int DropoffLocationId { get; set; }
        public decimal Price { get; set; }
        public bool IsExpired { get; set; }
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
            cargoAd.PickupLocationId = request.PickupLocationId;
            cargoAd.DropoffLocationId = request.DropoffLocationId;
            cargoAd.Price = request.Price;
            cargoAd.IsExpired = request.IsExpired;

            await _cargoAdService.UpdateAsync(cargoAd);
            return true;
        }
    }
} 