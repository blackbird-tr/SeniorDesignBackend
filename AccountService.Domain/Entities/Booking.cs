using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities
{

    public class Booking: BaseEntity
    {
    public int BookingId { get; set; }
    public int CustomerId { get; set; }
    public int CarrierId { get; set; }
    public int VehicleId { get; set; }
    public int CargoId { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime DropoffDate { get; set; }
    public double TotalPrice { get; set; }
    public string Status { get; set; }
    public bool IsFuelIncluded { get; set; }
      public virtual Cargo Cargo { get; set; }
       public virtual Vehicle Vehicle { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Carrier Carrier { get; set; }
        
    }
}