using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Location.Queries.GetByFullAddress
{
    public class GetLocationByFullAddressQuery : IRequest<LocationDto>
    {
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? PostalCode { get; set; }
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

    public class GetLocationByFullAddressQueryHandler : IRequestHandler<GetLocationByFullAddressQuery, LocationDto>
    {
        private readonly ILocationService _locationService;

        public GetLocationByFullAddressQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<LocationDto?> Handle(GetLocationByFullAddressQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationService.GetByFullAddressAsync(request.Address, request.City, request.State, request.PostalCode);
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
