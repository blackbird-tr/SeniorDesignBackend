using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Cargo.Commands.CreateCargo;

namespace AccountService.Application.Features.Cargo.Queries.GetByCargoType
{
    public class GetCargosByCargoTypeQuery : IRequest<List<CargoDto>>
    {
        public string CargoType { get; set; }
    }

    public class GetCargosByCargoTypeQueryHandler : IRequestHandler<GetCargosByCargoTypeQuery, List<CargoDto>>
    {
        private readonly ICargoService _cargoService;

        public GetCargosByCargoTypeQueryHandler(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        public async Task<List<CargoDto>> Handle(GetCargosByCargoTypeQuery request, CancellationToken cancellationToken)
        {
            var cargos = await _cargoService.GetByCargoTypeAsync(request.CargoType);
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
