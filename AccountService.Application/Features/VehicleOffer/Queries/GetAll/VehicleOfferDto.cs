using System;

namespace AccountService.Application.Features.VehicleOffer 
{
    public class VehicleOfferDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; } 
        public string ReceiverId { get; set; } 
        public int VehicleAdId { get; set; }
        public string VehicleAdTitle { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Admin1Id { get; set; }
        public string Admin2Id { get; set; }
        public string AdminStatus { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
} 