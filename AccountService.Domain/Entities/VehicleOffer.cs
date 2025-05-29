using AccountService.Domain.Enums;
using System;

namespace AccountService.Domain.Entities
{
    public class VehicleOffer : BaseEntity
    {
        public int Id { get; set; }
        public string SenderId { get; set; }  // Teklif veren kullanıcı
        public string ReceiverId { get; set; }  // Teklif alan kullanıcı
        public int VehicleAdId { get; set; }  // Araç ilanına verilen teklif
        public string Message { get; set; }    // Teklif mesajı
        public string Status { get; set; }   // Teklif durumu
        public DateTime? ExpiryDate { get; set; }  // Teklifin geçerlilik süresi
        public string Admin1Id { get; set; }  // İlk admin onayı
        public string Admin2Id { get; set; }  // İkinci admin onayı
        public byte AdminStatus { get; set; }      // Teklif durumu (byte)

        // Navigation properties
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public VehicleAd VehicleAd { get; set; }
    }
} 