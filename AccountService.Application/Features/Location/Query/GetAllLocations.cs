using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Location.Queries.GetAll
{
    public class GetAllLocationsQuery : IRequest<List<LocationDto>> { }

    public class LocationDto
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? PostalCode { get; set; }
        public string? Coordinates { get; set; }
    }

    public class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery, List<LocationDto>>
    {
        private readonly ILocationService _locationService;

        public GetAllLocationsQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<List<LocationDto>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _locationService.GetAllAsync();
            return locations
                .Where(l => l.Active)
                .Select(location => new LocationDto
                {
                    Id = location.Id,
                    Address = location.Address,
                    City = location.City,
                    State = location.State,
                    PostalCode = location.PostalCode,
                    Coordinates = location.Coordinates
                })
                .ToList();
        }
    }
}
