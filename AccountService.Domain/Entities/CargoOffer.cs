using AccountService.Domain.Enums;
using System;

namespace AccountService.Domain.Entities
{
    public class CargoOffer : BaseEntity
    {
        public int Id { get; set; }
        public string SenderId { get; set; }  // Teklif veren kullanıcı
        public string ReceiverId { get; set; }  // Teklif alan kullanıcı
        public int CargoAdId { get; set; }    // Kargo ilanına verilen teklif
        public decimal Price { get; set; }     // Teklif edilen fiyat
        public string Message { get; set; }    // Teklif mesajı
        public string Status { get; set; }   // Teklif durumu
        public DateTime? ExpiryDate { get; set; }  // Teklifin geçerlilik süresi

        // Navigation properties
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public CargoAd CargoAd { get; set; }
    }
} 