using AccountService.Domain.Entities;
using AccountService.Application.Features.Notification.Queries.GetAll;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface INotificationService
    {
        Task<Notification> CreateNotificationAsync(string username, string title, string message, NotificationType type, int? relatedEntityId = null);
        Task<List<NotificationDto>> GetUserNotificationsAsync(string username);
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(string username);
        Task<int> GetUnreadCountAsync(string username);
    }
} 