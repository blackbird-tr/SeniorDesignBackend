namespace AccountService.Domain.Enums
{
    public enum CargoOfferStatus
    {
        Pending = 0,    // Beklemede
        Accepted = 1,   // Kabul Edildi
        Rejected = 2,   // Reddedildi
        Cancelled = 3,  // İptal Edildi
        Expired = 4,    // Süresi Doldu
        Completed = 5   // Tamamlandı
    }
} 