using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Location.Commands.CreateLocation;

namespace AccountService.Application.Features.Location.Queries.GetByCoordinates
{
    public class GetLocationByCoordinatesQuery : IRequest<LocationDto>
    {
        public string Coordinates { get; set; }
    }

    public class GetLocationByCoordinatesQueryHandler : IRequestHandler<GetLocationByCoordinatesQuery, LocationDto>
    {
        private readonly ILocationService _locationService;

        public GetLocationByCoordinatesQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<LocationDto?> Handle(GetLocationByCoordinatesQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationService.GetByCoordinatesAsync(request.Coordinates);
            if (location == null) return null;

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
