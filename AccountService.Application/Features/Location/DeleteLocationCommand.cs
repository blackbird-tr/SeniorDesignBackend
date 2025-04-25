using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application 
{
    public class DeleteLocationCommand : IRequest
    {
        public int LocationId { get; set; }
    }

    public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand>
    {
        private readonly ILocationRepository _locationRepository;

        public DeleteLocationCommandHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Unit> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetByIdAsync(request.LocationId);
            if (location == null) throw new Exception("Location not found");

            await _locationRepository.DeleteAsync(location);
            return Unit.Value;
        }
    }
}