using System;

namespace AccountService.Application.Features.CargoOffer 
{
    public class CargoOfferDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; } 
        public string ReceiverId { get; set; } 
        public int CargoAdId { get; set; }
        public string CargoAdTitle { get; set; }
        public decimal Price { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Admin1Id { get; set; }  // İlk admin onayı
        public string Admin2Id { get; set; }  // İkinci admin onayı
        public string AdminStatus { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
} 