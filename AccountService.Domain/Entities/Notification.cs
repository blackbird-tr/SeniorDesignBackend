
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities
{
    public class Notification : BaseEntity
    {
    public int NotificationId { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }
    }
}