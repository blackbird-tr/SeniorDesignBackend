using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application
{
    public class UpdateLocationCommand : IRequest
    {
        public int LocationId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public string Coordinates { get; set; }
    }

    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand>
    {
        private readonly ILocationRepository _locationRepository;

        public UpdateLocationCommandHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Unit> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetByIdAsync(request.LocationId);
            if (location == null) throw new Exception("Location not found");

            location.Address = request.Address;
            location.City = request.City;
            location.State = request.State;
            location.PostalCode = request.PostalCode;
            location.Coordinates = request.Coordinates;

            await _locationRepository.UpdateAsync(location);
            return Unit.Value;
        }
    }
}