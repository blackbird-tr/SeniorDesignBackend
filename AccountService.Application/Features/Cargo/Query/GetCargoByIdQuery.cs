using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Cargo.Queries.GetById
{
    public class GetCargoByIdQuery : IRequest<CargoDto>
    {
        public int Id { get; set; }
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

    public class GetCargoByIdQueryHandler : IRequestHandler<GetCargoByIdQuery, CargoDto>
    {
        private readonly ICargoService _cargoService;

        public GetCargoByIdQueryHandler(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        public async Task<CargoDto?> Handle(GetCargoByIdQuery request, CancellationToken cancellationToken)
        {
            var cargo = await _cargoService.GetByIdAsync(request.Id);
            if (cargo == null || !cargo.Active) return null;

            return new CargoDto
            {
                Id = cargo.Id,
                Description = cargo.Description,
                Weight = cargo.Weight,
                CargoType = cargo.CargoType,
                Status = cargo.Status
            };
        }
    }
}
