using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Cargo.Queries.GetAll
{
    public class GetAllCargosQuery : IRequest<List<CargoDto>> { }

    public class CargoDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public float? Weight { get; set; }
        public string? CargoType { get; set; }
        public byte Status { get; set; }
        public string StatusText => ((Domain.Enums.CargoStatus)Status).ToString();
    }

    public class GetAllCargosQueryHandler : IRequestHandler<GetAllCargosQuery, List<CargoDto>>
    {
        private readonly ICargoService _cargoService;

        public GetAllCargosQueryHandler(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        public async Task<List<CargoDto>> Handle(GetAllCargosQuery request, CancellationToken cancellationToken)
        {
            var cargos = await _cargoService.GetAllAsync();
            return cargos
                .Where(c => c.Active)
                .Select(c => new CargoDto
                {
                    Id = c.Id,
                    Description = c.Description,
                    Weight = c.Weight,
                    CargoType = c.CargoType,
                    Status = c.Status
                })
                .ToList();
        }
    }
}
