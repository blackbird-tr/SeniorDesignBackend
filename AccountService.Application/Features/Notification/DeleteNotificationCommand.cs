using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application
{
    public class DeleteNotificationCommand : IRequest
    {
        public int NotificationId { get; set; }
    }

    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand>
    {
        private readonly INotificationRepository _notificationRepository;

        public DeleteNotificationCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Unit> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetByIdAsync(request.NotificationId);
            if (notification == null) throw new Exception("Notification not found");

            await _notificationRepository.DeleteAsync(notification);
            return Unit.Value;
        }
    }
}