using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using MediatR;
using System;

namespace AccountService.Application
{
    public class CreateNotificationCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, int>
    {
        private readonly INotificationRepository _notificationRepository;

        public CreateNotificationCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<int> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                UserId = request.UserId,
                Message = request.Message,
                CreatedAt = request.CreatedAt
            };

            await _notificationRepository.AddAsync(notification);
            return notification.NotificationId;
        }
    }
}