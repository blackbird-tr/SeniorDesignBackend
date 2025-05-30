using Microsoft.AspNetCore.Mvc;
using AccountService.WebApi.Controller;
using AccountService.Application.Interfaces;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AccountService.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : BaseApiController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserNotifications()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var notifications = await _notificationService.GetUserNotificationsAsync(username);
            return Ok(notifications);
        }

        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var count = await _notificationService.GetUnreadCountAsync(username);
            return Ok(new { Count = count });
        }

        [HttpPut("{id}/mark-as-read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            await _notificationService.MarkAsReadAsync(id);
            return NoContent();
        }

        [HttpPut("mark-all-as-read")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            await _notificationService.MarkAllAsReadAsync(username);
            return NoContent();
        }
    }
} 