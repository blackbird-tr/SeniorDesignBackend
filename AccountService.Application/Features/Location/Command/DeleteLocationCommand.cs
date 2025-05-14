using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Location.Commands.DeleteLocation
{
    public class DeleteLocationCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, bool>
    {
        private readonly ILocationService _locationService;

        public DeleteLocationCommandHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<bool> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationService.GetByIdAsync(request.Id);
            if (location == null) return false;

            location.Active = false;
            await _locationService.UpdateAsync(location);
            return true;
        }
    }
}
