using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Location.Commands.CreateLocation;

namespace AccountService.Application.Features.Location.Queries.GetByPostalCode
{
    public class GetLocationsByPostalCodeQuery : IRequest<List<LocationDto>>
    {
        public int PostalCode { get; set; }
    }

    public class GetLocationsByPostalCodeQueryHandler : IRequestHandler<GetLocationsByPostalCodeQuery, List<LocationDto>>
    {
        private readonly ILocationService _locationService;

        public GetLocationsByPostalCodeQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<List<LocationDto>> Handle(GetLocationsByPostalCodeQuery request, CancellationToken cancellationToken)
        {
            var locations = await _locationService.GetByPostalCodeAsync(request.PostalCode);

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
