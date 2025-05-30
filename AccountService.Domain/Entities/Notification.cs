using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities
{
    public class Notification : BaseEntity
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Message { get; set; }

        [Required]
        public NotificationType Type { get; set; }

        public int? RelatedEntityId { get; set; }

        public bool IsRead { get; set; }

        // Navigation
        public User User { get; set; }
    }

    public enum NotificationType
    {
        VehicleOffer,
        CargoOffer
    }
}