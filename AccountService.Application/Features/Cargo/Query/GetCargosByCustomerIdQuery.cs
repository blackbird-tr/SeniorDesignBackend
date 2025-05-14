using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Cargo.Queries.GetByCustomerId
{
    public class GetCargosByCustomerIdQuery : IRequest<List<CargoDto>>
    {
        public int CustomerId { get; set; }
    }

    public class CargoDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public float? Weight { get; set; }
        public string? CargoType { get; set; }
        public byte Status { get; set; }
        public string StatusText => ((Domain.Enums.CargoStatus)Status).ToString();
    }

    public class GetCargosByCustomerIdQueryHandler : IRequestHandler<GetCargosByCustomerIdQuery, List<CargoDto>>
    {
        private readonly ICargoService _cargoService;

        public GetCargosByCustomerIdQueryHandler(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        public async Task<List<CargoDto>> Handle(GetCargosByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var cargos = await _cargoService.GetByCustomerIdAsync(request.CustomerId);
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
