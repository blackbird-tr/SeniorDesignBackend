using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountService.Domain.Entities
{
    public class Carrier : BaseEntity
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string LicenseNumber { get; set; }

        [Required]
        public bool AvailabilityStatus { get; set; }

        // Navigation properties
          [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}