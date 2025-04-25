using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application
{
    public class DeleteCargoCommand : IRequest
    {
        public int CargoId { get; set; }
    }

    public class DeleteCargoCommandHandler : IRequestHandler<DeleteCargoCommand>
    {
        private readonly ICargoRepository _cargoRepository;

        public DeleteCargoCommandHandler(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        public async Task<Unit> Handle(DeleteCargoCommand request, CancellationToken cancellationToken)
        {
            var cargo = await _cargoRepository.GetByIdAsync(request.CargoId);
            if (cargo == null) throw new Exception("Cargo not found");

            await _cargoRepository.DeleteAsync(cargo);
            return Unit.Value;
        }
    }
}