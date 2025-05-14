using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.Location.Commands.CreateLocation
{
    public class CreateLocationCommand : IRequest<LocationDto>
    {
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? PostalCode { get; set; }
        public string? Coordinates { get; set; }
    }

    public class LocationDto
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? PostalCode { get; set; }
        public string? Coordinates { get; set; }
    }

    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, LocationDto>
    {
        private readonly ILocationService _locationService;

        public CreateLocationCommandHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<LocationDto> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = new Domain.Entities.Location
            {
                Address = request.Address,
                City = request.City,
                State = request.State,
                PostalCode = request.PostalCode,
                Coordinates = request.Coordinates,
                Active = true
            };

            var created = await _locationService.AddAsync(location);

            return new LocationDto
            {
                Id = created.Id,
                Address = created.Address,
                City = created.City,
                State = created.State,
                PostalCode = created.PostalCode,
                Coordinates = created.Coordinates
            };
        }
    }
}
