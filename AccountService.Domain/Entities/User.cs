using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AccountService.Domain.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public int  BirthYear { get; set; }

        [MaxLength(100)]
        public string? CompanyName { get; set; }


        public ICollection<Vehicle> Vehicles { get; set; }

        public ICollection<CargoAd> Cargos { get; set; }  
    }
}