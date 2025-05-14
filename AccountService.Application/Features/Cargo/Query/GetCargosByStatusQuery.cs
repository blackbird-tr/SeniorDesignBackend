using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Cargo.Queries.GetAll;

namespace AccountService.Application.Features.Cargo.Queries.GetByStatus
{
    public class GetCargosByStatusQuery : IRequest<List<CargoDto>>
    {
        public byte Status { get; set; }
    }

    public class GetCargosByStatusQueryHandler : IRequestHandler<GetCargosByStatusQuery, List<CargoDto>>
    {
        private readonly ICargoService _cargoService;

        public GetCargosByStatusQueryHandler(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        public async Task<List<CargoDto>> Handle(GetCargosByStatusQuery request, CancellationToken cancellationToken)
        {
            var cargos = await _cargoService.GetByStatusAsync(request.Status);
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
