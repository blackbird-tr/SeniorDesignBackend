namespace AccountService.Application.Common.DTOs.User
{
    public class AllUsersResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool IsLocked { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
} 