using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Location.Commands.UpdateLocation
{
    public class UpdateLocationCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? PostalCode { get; set; }
        public string? Coordinates { get; set; }
    }

    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, bool>
    {
        private readonly ILocationService _locationService;

        public UpdateLocationCommandHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<bool> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationService.GetByIdAsync(request.Id);
            if (location == null) return false;

            location.Address = request.Address;
            location.City = request.City;
            location.State = request.State;
            location.PostalCode = request.PostalCode;
            location.Coordinates = request.Coordinates;

            await _locationService.UpdateAsync(location);
            return true;
        }
    }
}
