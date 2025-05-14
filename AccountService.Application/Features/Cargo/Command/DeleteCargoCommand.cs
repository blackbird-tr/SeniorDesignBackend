using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Cargo.Commands.DeleteCargo
{
    public class DeleteCargoCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteCargoCommandHandler : IRequestHandler<DeleteCargoCommand, bool>
    {
        private readonly ICargoService _cargoService;

        public DeleteCargoCommandHandler(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        public async Task<bool> Handle(DeleteCargoCommand request, CancellationToken cancellationToken)
        {
            var cargo = await _cargoService.GetByIdAsync(request.Id);
            if (cargo == null) return false;

            cargo.Active = false;
            await _cargoService.UpdateAsync(cargo);
            return true;
        }
    }
}
