using System;
using System.ComponentModel.DataAnnotations;

namespace AccountService.Domain.Entities
{
    public class Feedback : BaseEntity
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public int UserId { get; set; }

        public float? Rating { get; set; }

        [MaxLength(500)]
        public string? Comment { get; set; }

        public DateTime? Date { get; set; }

        // Navigation properties
        public Booking Booking { get; set; }
        public User User { get; set; }
    }
}