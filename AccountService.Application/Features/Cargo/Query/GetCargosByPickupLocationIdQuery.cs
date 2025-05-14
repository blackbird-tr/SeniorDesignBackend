using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Cargo.Commands.CreateCargo;

namespace AccountService.Application.Features.Cargo.Queries.GetByPickupLocation
{
    public class GetCargosByPickupLocationIdQuery : IRequest<List<CargoDto>>
    {
        public int PickupLocationId { get; set; }
    }

    public class GetCargosByPickupLocationIdQueryHandler : IRequestHandler<GetCargosByPickupLocationIdQuery, List<CargoDto>>
    {
        private readonly ICargoService _cargoService;

        public GetCargosByPickupLocationIdQueryHandler(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        public async Task<List<CargoDto>> Handle(GetCargosByPickupLocationIdQuery request, CancellationToken cancellationToken)
        {
            var cargos = await _cargoService.GetByPickupLocationIdAsync(request.PickupLocationId);
            return cargos
                .Where(c => c.Active)
                .Select(c => new CargoDto
                {
                    Id = c.Id,
                    Description = c.Description,
                    Weight = c.Weight,
                    CargoType = c.CargoType,
                    Status = c.Status
                }).ToList();
        }
    }
}
