using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccountService.Domain.Enums;

namespace AccountService.Domain.Entities
{
    public class Booking : BaseEntity
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int CarrierId { get; set; }

        [Required]
        public int VehicleId { get; set; }

        [Required]
        public int CargoId { get; set; }

        public DateTime? PickupDate { get; set; }

        public DateTime? DropoffDate { get; set; }

        public float? TotalPrice { get; set; }

        public byte Status { get; set; }

        public bool IsFuelIncluded { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public Carrier Carrier { get; set; }
        public Vehicle Vehicle { get; set; }
        public Cargo Cargo { get; set; }
        public Payment Payment { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();

        // Enum property
        [NotMapped]
        public BookingStatus BookingStatus
        {
            get => (BookingStatus)Status;
            set => Status = (byte)value;
        }
    }
}