using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities
{
    public class VehicleAd : BaseEntity
    { 

        public string   Title   { get; set; }
        public string Desc { get; set; } 
        [Required]
        public string userId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        [Required]
        public string VehicleType { get; set; }

        [Required]
        public float Capacity { get; set; }
        
        [Required]
        public DateTime AdDate { get; set; }
         
          
        // Navigation properties
        public User Carrier { get; set; }

    }
}
