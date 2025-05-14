using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Location.Queries.GetById
{
    public class GetLocationByIdQuery : IRequest<LocationDto>
    {
        public int Id { get; set; }
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

    public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, LocationDto>
    {
        private readonly ILocationService _locationService;

        public GetLocationByIdQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<LocationDto> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationService.GetByIdAsync(request.Id);
            if (location == null || !location.Active) return null;

            return new LocationDto
            {
                Id = location.Id,
                Address = location.Address,
                City = location.City,
                State = location.State,
                PostalCode = location.PostalCode,
                Coordinates = location.Coordinates
            };
        }
    }
}
