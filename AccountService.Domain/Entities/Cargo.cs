using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccountService.Domain.Enums;

namespace AccountService.Domain.Entities
{
    public class Cargo : BaseEntity
    {
        [Required]
        public int CustomerId { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        public float? Weight { get; set; }

        [MaxLength(50)]
        public string? CargoType { get; set; }

        [Required]
        public int PickupLocationId { get; set; }

        [Required]
        public int DropoffLocationId { get; set; }

        public byte Status { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public Location PickupLocation { get; set; }
        public Location DropoffLocation { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();

        // Enum property
        [NotMapped]
        public CargoStatus CargoStatus
        {
            get => (CargoStatus)Status;
            set => Status = (byte)value;
        }
    }
}