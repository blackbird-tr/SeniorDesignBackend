using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Cargo.Commands.CreateCargo;

namespace AccountService.Application.Features.Cargo.Queries.GetByWeightRange
{
    public class GetCargosByWeightRangeQuery : IRequest<List<CargoDto>>
    {
        public float MinWeight { get; set; }
        public float MaxWeight { get; set; }
    }

    public class GetCargosByWeightRangeQueryHandler : IRequestHandler<GetCargosByWeightRangeQuery, List<CargoDto>>
    {
        private readonly ICargoService _cargoService;

        public GetCargosByWeightRangeQueryHandler(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        public async Task<List<CargoDto>> Handle(GetCargosByWeightRangeQuery request, CancellationToken cancellationToken)
        {
            var cargos = await _cargoService.GetByWeightRangeAsync(request.MinWeight, request.MaxWeight);
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
