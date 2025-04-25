
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities
{
    public class Payment : BaseEntity
    {
    public int PaymentId { get; set; }
    public int BookingId { get; set; }
    public double Amount { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Status { get; set; }
        public virtual Booking Booking { get; set; }
    }
}