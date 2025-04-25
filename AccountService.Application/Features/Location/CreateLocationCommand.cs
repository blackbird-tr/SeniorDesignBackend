using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using MediatR;

namespace AccountService.Application { 
    public class CreateLocationCommand : IRequest<int>
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public string Coordinates { get; set; }
    }

    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, int>
    {
        private readonly ILocationRepository _locationRepository;

        public CreateLocationCommandHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<int> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = new Location
            {
                Address = request.Address,
                City = request.City,
                State = request.State,
                PostalCode = request.PostalCode,
                Coordinates = request.Coordinates
            };

            await _locationRepository.AddAsync(location);
            return location.LocationId;
        }
    }
}