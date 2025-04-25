using AccountService.Application.Interfaces;
using MediatR;
using System;

namespace AccountService.Application
{
    public class UpdateNotificationCommand : IRequest
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand>
    {
        private readonly INotificationRepository _notificationRepository;

        public UpdateNotificationCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Unit> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetByIdAsync(request.NotificationId);
            if (notification == null) throw new Exception("Notification not found");

            notification.UserId = request.UserId;
            notification.Message = request.Message;
            notification.CreatedAt = request.CreatedAt;

            await _notificationRepository.UpdateAsync(notification);
            return Unit.Value;
        }
    }
}