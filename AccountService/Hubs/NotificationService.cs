using System.Collections.Concurrent;

namespace AccountService.Hubs
{
    public class NotificationService
    {
        private readonly ConcurrentDictionary<string, string> _onlineUsers = new();

        public void AddUser(string userId, string connectionId)
        {
            _onlineUsers[userId] = connectionId;
        }

        public void RemoveUser(string userId)
        {
            _onlineUsers.TryRemove(userId, out _);
        }

        public IReadOnlyDictionary<string, string> GetOnlineUsers()
        {
            return _onlineUsers;
        }
    }
} 