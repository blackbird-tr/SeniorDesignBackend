
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
    public int VehicleId { get; set; }
    public int CarrierId { get; set; }
    public int VehicleTypeId { get; set; }
    public double Capacity { get; set; }
    public string LicensePlate { get; set; }
    public bool AvailabilityStatus { get; set; }
    public string Model { get; set; }
        public virtual Carrier Carrier { get; set; }
        public virtual VehicleType VehicleType { get; set; }
    }
}