using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.CargoAd.Commands.Delete
{
    public class DeleteCargoAdCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteCargoAdCommandHandler : IRequestHandler<DeleteCargoAdCommand, bool>
    {
        private readonly ICargoAdService _cargoAdService;

        public DeleteCargoAdCommandHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<bool> Handle(DeleteCargoAdCommand request, CancellationToken cancellationToken)
        {
            var cargoAd = await _cargoAdService.GetByIdAsync(request.Id);
            if (cargoAd == null) return false;

            cargoAd.Active = false;
            await _cargoAdService.UpdateAsync(cargoAd);
            return true;
        }
    }
} 