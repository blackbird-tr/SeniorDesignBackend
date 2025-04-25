
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities
{
    public class Carrier : BaseEntity
    {
    public int CarrierId { get; set; }
    public int VehicleTypeId { get; set; }
    public string LicenseNumber { get; set; }
    public bool AvailabilityStatus { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        
    }
}