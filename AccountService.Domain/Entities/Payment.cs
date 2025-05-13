using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AccountService.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountService.Domain.Entities
{
    public class Payment : BaseEntity
    {
        [Required]
        public int BookingId { get; set; }

        public float? Amount { get; set; }

        [MaxLength(50)]
        public string? PaymentMethod { get; set; }

        public DateTime? PaymentDate { get; set; }

        public byte Status { get; set; }

        // Navigation properties
        public Booking Booking { get; set; }

        // Enum property
        [NotMapped]
        public PaymentStatus PaymentStatus
        {
            get => (PaymentStatus)Status;
            set => Status = (byte)value;
        }
    }
}