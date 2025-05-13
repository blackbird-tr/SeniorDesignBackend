using System.ComponentModel.DataAnnotations;

namespace AccountService.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        [Required]
        public int CarrierId { get; set; }

        [Required]
        public int VehicleTypeId { get; set; }

        [Required]
        public float Capacity { get; set; }

        [Required]
        [MaxLength(20)]
        public string LicensePlate { get; set; }

        [Required]
        public bool AvailabilityStatus { get; set; }

        [MaxLength(100)]
        public string? Model { get; set; }

        // Navigation properties
        public Carrier Carrier { get; set; }
        public VehicleType VehicleType { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}