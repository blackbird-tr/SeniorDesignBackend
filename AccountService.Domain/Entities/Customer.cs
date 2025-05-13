using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountService.Domain.Entities
{
    public class Customer : BaseEntity
    {
        [Required]
        public string UserId { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        // Navigation properties
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public ICollection<Cargo> Cargos { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}