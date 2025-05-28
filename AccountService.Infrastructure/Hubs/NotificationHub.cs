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
            // Kullanıcı bağlandığında kendi ID'sine göre gruba eklenir
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
            // Kullanıcı bağlantısı kesildiğinde gruptan çıkarılır
            var userId = Context.User?.FindFirst("uid")?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
                _notificationService.RemoveUser(userId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
} 