using System.ComponentModel.DataAnnotations;

namespace AccountService.Domain.Entities
{
    public class VehicleType : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        // Navigation properties
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}