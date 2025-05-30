using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AccountService.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly NotificationService _notificationService;

        public NotificationHub(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public override async Task OnConnectedAsync()
        {
            // Kullan?c? ba?land???nda kendi ID'sine göre gruba eklenir
            var userId = Context.User?.FindFirst("uid")?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
                _notificationService.AddUser(userId, Context.ConnectionId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // Kullan?c? ba?lant?s? kesildi?inde gruptan ç?kar?l?r
            var userId = Context.User?.FindFirst("uid")?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
                _notificationService.RemoveUser(userId, Context.ConnectionId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
} 