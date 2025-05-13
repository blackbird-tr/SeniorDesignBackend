namespace AccountService.Domain.Enums
{
    public enum PaymentStatus : byte
    {
        Pending = 0,
        Completed = 1,
        Failed = 2,
        Refunded = 3
    }

    public class PaymentStatusInfo
    {
        public PaymentStatus Value { get; set; }
        public required string Text { get; set; }
    }

    public class PaymentStatuses
    {
        public PaymentStatusInfo[] GetStatuses()
        {
            return
            [
                new PaymentStatusInfo { Value = PaymentStatus.Pending, Text = "Beklemede" },
                new PaymentStatusInfo { Value = PaymentStatus.Completed, Text = "Tamamlandı" },
                new PaymentStatusInfo { Value = PaymentStatus.Failed, Text = "Başarısız" },
                new PaymentStatusInfo { Value = PaymentStatus.Refunded, Text = "İade Edildi" }
            ];
        }

        public string GetName(PaymentStatus value)
        {
            return GetStatuses().First(p => p.Value == value).Text;
        }
    }
} 