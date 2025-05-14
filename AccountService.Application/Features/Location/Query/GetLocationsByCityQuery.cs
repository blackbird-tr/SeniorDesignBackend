using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Location.Commands.CreateLocation;

namespace AccountService.Application.Features.Location.Queries.GetByCity
{
    public class GetLocationsByCityQuery : IRequest<List<LocationDto>>
    {
        public string City { get; set; }
    }

    public class GetLocationsByCityQueryHandler : IRequestHandler<GetLocationsByCityQuery, List<LocationDto>>
    {
        private readonly ILocationService _locationService;

        public GetLocationsByCityQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<List<LocationDto>> Handle(GetLocationsByCityQuery request, CancellationToken cancellationToken)
        {
            var locations = await _locationService.GetByCityAsync(request.City);

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
