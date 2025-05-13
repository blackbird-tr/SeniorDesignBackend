using System.ComponentModel.DataAnnotations;

namespace AccountService.Domain.Entities
{
    public class Location : BaseEntity
    {
        [MaxLength(255)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        public int? PostalCode { get; set; }

        [MaxLength(100)]
        public string? Coordinates { get; set; }

        // Navigation properties
        public ICollection<Cargo> PickupLocations { get; set; }
        public ICollection<Cargo> DropoffLocations { get; set; }
    }
}