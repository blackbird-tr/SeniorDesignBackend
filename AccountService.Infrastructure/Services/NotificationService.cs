using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using AccountService.Infrastructure.Hubs;
using AccountService.Application.Features.Notification.Queries.GetAll;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(
            ApplicationDbContext context,
            IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<Notification> CreateNotificationAsync(string username, string title, string message, NotificationType type, int? relatedEntityId = null)
        {
            var notification = new Notification
            {
                UserId = username,
                Title = title,
                Message = message,
                Type = type,
                RelatedEntityId = relatedEntityId,
                IsRead = false,
                Active = true
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            // SignalR ile real-time bildirim gönder
            await _hubContext.Clients.Group(username).SendAsync("ReceiveNotification", notification);

            return notification;
        }

        public async Task<List<NotificationDto>> GetUserNotificationsAsync(string username)
        {
            return await _context.Notifications
                .Where(n => n.UserId == username && n.Active)
                .OrderByDescending(n => n.CreatedDate)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Message = n.Message,
                    Type = n.Type,
                    IsRead = n.IsRead,
                    CreatedDate = n.CreatedDate,
                    RelatedEntityId = n.RelatedEntityId
                })
                .ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAllAsReadAsync(string username)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == username && !n.IsRead && n.Active)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<int> GetUnreadCountAsync(string username)
        {
            return await _context.Notifications
                .CountAsync(n => n.UserId == username && !n.IsRead && n.Active);
        }
    }
} 