

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities
{
    public class Feedback : BaseEntity
    {
    public int FeedbackId { get; set; }
    public int BookingId { get; set; }
    public int UserId { get; set; }
    public double Rating { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual User User { get; set; }
    }
}