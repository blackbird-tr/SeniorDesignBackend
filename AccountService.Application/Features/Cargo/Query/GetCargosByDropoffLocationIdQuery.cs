using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Cargo.Queries.GetAll;

namespace AccountService.Application.Features.Cargo.Queries.GetByDropoffLocation
{
    public class GetCargosByDropoffLocationIdQuery : IRequest<List<CargoDto>>
    {
        public int DropoffLocationId { get; set; }
    }

    public class GetCargosByDropoffLocationIdQueryHandler : IRequestHandler<GetCargosByDropoffLocationIdQuery, List<CargoDto>>
    {
        private readonly ICargoService _cargoService;

        public GetCargosByDropoffLocationIdQueryHandler(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        public async Task<List<CargoDto>> Handle(GetCargosByDropoffLocationIdQuery request, CancellationToken cancellationToken)
        {
            var cargos = await _cargoService.GetByDropoffLocationIdAsync(request.DropoffLocationId);
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
