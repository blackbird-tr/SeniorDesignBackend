using System;

namespace AccountService.Domain.Entities
{
    public class Admin
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
} 