namespace AccountService.Domain.Enums
{
    public enum BookingStatus : byte
    {
        Pending = 0,
        Confirmed = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4
    }

    public class BookingStatusInfo
    {
        public BookingStatus Value { get; set; }
        public required string Text { get; set; }
    }

    public class BookingStatuses
    {
        public BookingStatusInfo[] GetStatuses()
        {
            return
            [
                new BookingStatusInfo { Value = BookingStatus.Pending, Text = "Beklemede" },
                new BookingStatusInfo { Value = BookingStatus.Confirmed, Text = "Onaylandı" },
                new BookingStatusInfo { Value = BookingStatus.InProgress, Text = "Devam Ediyor" },
                new BookingStatusInfo { Value = BookingStatus.Completed, Text = "Tamamlandı" },
                new BookingStatusInfo { Value = BookingStatus.Cancelled, Text = "İptal Edildi" }
            ];
        }

        public string GetName(BookingStatus value)
        {
            return GetStatuses().First(p => p.Value == value).Text;
        }
    }
} 