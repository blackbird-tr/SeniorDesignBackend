using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Cargo.Commands.ChangeStatus
{
    public class ChangeCargoStatusCommand : IRequest<bool>
    {
        public int CargoId { get; set; }
        public byte Status { get; set; }  
    }

    public class ChangeCargoStatusCommandHandler : IRequestHandler<ChangeCargoStatusCommand, bool>
    {
        private readonly ICargoService _cargoService;

        public ChangeCargoStatusCommandHandler(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        public async Task<bool> Handle(ChangeCargoStatusCommand request, CancellationToken cancellationToken)
        {
            var cargo = await _cargoService.GetByIdAsync(request.CargoId);
            if (cargo == null || !cargo.Active)
                return false;

            cargo.Status = request.Status;
            await _cargoService.UpdateAsync(cargo);
            return true;
        }
    }
}
