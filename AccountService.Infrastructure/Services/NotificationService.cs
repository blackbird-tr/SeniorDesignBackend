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
using Microsoft.AspNetCore.Identity;

namespace AccountService.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly UserManager<User> _userManager;

        public NotificationService(
            ApplicationDbContext context,
            IHubContext<NotificationHub> hubContext,
            UserManager<User> userManager)
        {
            _context = context;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        public async Task<Notification> CreateNotificationAsync(string userId, string title, string message, NotificationType type, int? relatedEntityId = null)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var notification = new Notification
            {
                UserId = user.Id,
                Title = title,
                Message = message,
                Type = type,
                RelatedEntityId = relatedEntityId,
                IsRead = false,
                Active = true,
                CreatedDate = DateTime.UtcNow
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            // SignalR ile kullanıcıya özel real-time bildirim gönder
            //await _hubContext.Clients.User(user.Id).SendAsync("ReceiveNotification", notification);
            try
            {
                await _hubContext.Clients.Group($"user_{user.Id}")
                    .SendAsync("ReceiveNotification", new
                    {
                        Id = notification.Id,
                        Title = notification.Title,
                        Message = notification.Message,
                        Type = notification.Type,
                        CreatedDate = notification.CreatedDate
                    });
            }
            catch (Exception ex)
            {
                // Logging önerilir, ama notification DB'ye yazıldığı için kayıp olmaz.
            }

            return notification;
        }



        public async Task<List<NotificationDto>> GetUserNotificationsAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new Exception("User not found");

            return await _context.Notifications
                .Where(n => n.UserId == user.Id && n.Active)
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
                _context.Update(notification);
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAllAsReadAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new Exception("User not found");

            var notifications = await _context.Notifications
                .Where(n => n.UserId == user.Id && !n.IsRead && n.Active)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
                _context.Update(notification);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<int> GetUnreadCountAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new Exception("User not found");

            return await _context.Notifications
                .CountAsync(n => n.UserId == user.Id && !n.IsRead && n.Active);
        }
    }
} 