using System;
using AccountService.Domain.Entities;
using AccountService.Domain.Enums;

namespace AccountService.Application.Features.Notification.Queries.GetAll
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? RelatedEntityId { get; set; }
    }
} 