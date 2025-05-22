using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace AccountService.Domain.Entities
{
    public class CargoAd : BaseEntity
    {
        [Required]
        public string UserId { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        public float? Weight { get; set; }

        [MaxLength(50)]
        public string? CargoType { get; set; }

        [Required]
        public int PickupLocationId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int DropoffLocationId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool IsExpired { get; set; }

        // Navigation properties
        public User Customer { get; set; }
        public Location PickupLocation { get; set; }
        public Location DropoffLocation { get; set; } 
    }
}