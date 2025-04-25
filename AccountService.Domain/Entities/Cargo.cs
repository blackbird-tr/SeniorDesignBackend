
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities
{
    public class Cargo : BaseEntity
    {
    public int CargoId { get; set; }
    public int CustomerId { get; set; }
    public string Desc { get; set; }
    public double Weight { get; set; }
    public string CargoType { get; set; }
    public string PickUpLocation { get; set; }
    public string DropOffLocation { get; set; }
    public string Status { get; set; }
        public virtual Customer Customer { get; set; }
        
    }
}