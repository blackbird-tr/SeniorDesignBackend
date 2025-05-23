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
        public string Title { get; set; }
        public string DropCountry { get; set; }
        public string DropCity { get; set; }
        public string PickCountry { get; set; }
        public string PickCity { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string currency { get; set; }

        public bool IsExpired { get; set; }

        // Navigation properties
        public User Customer { get; set; } 
    }
}