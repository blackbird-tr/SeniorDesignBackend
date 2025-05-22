using System.ComponentModel.DataAnnotations;

namespace AccountService.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        [Required]
        public string userId { get; set; }
        public string Title { get; set; }

        [Required]
        public string VehicleType { get; set; }

        [Required]
        public float Capacity { get; set; }

        [Required]
        [MaxLength(20)]
        public string LicensePlate { get; set; }
         

        [MaxLength(100)]
        public string? Model { get; set; }

        // Navigation properties
        public User Carrier { get; set; } 
    }
}