using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Location.Commands.CreateLocation;

namespace AccountService.Application.Features.Location.Queries.GetByRegion
{
    public class GetLocationsByRegionQuery : IRequest<List<LocationDto>>
    {
        public string City { get; set; }
        public string State { get; set; }
    }

    public class GetLocationsByRegionQueryHandler : IRequestHandler<GetLocationsByRegionQuery, List<LocationDto>>
    {
        private readonly ILocationService _locationService;

        public GetLocationsByRegionQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<List<LocationDto>> Handle(GetLocationsByRegionQuery request, CancellationToken cancellationToken)
        {
            var locations = await _locationService.GetByRegionAsync(request.City, request.State);

            return locations.Select(l => new LocationDto
            {
                Id = l.Id,
                Address = l.Address,
                City = l.City,
                State = l.State,
                PostalCode = l.PostalCode,
                Coordinates = l.Coordinates
            }).ToList();
        }
    }
}
