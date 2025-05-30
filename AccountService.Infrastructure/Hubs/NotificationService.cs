using System.Collections.Concurrent;

namespace AccountService.Hubs
{
    public class NotificationService
    {
        private readonly ConcurrentDictionary<string, ConcurrentBag<string>> _onlineUsers = new();

        public void AddUser(string userId, string connectionId)
        {
            _onlineUsers.AddOrUpdate(
                userId,
                new ConcurrentBag<string> { connectionId },
                (key, existingList) =>
                {
                    existingList.Add(connectionId);
                    return existingList;
                });
        }

        public void RemoveUser(string userId, string connectionId)
        {
            if (_onlineUsers.TryGetValue(userId, out var connections))
            {
                var updatedList = new ConcurrentBag<string>(connections.Where(c => c != connectionId));
                if (updatedList.IsEmpty)
                {
                    _onlineUsers.TryRemove(userId, out _);
                }
                else
                {
                    _onlineUsers[userId] = updatedList;
                }
            }
        }

        public IReadOnlyDictionary<string, IEnumerable<string>> GetOnlineUsers()
        {
            return _onlineUsers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.AsEnumerable());
        }
    }
}
