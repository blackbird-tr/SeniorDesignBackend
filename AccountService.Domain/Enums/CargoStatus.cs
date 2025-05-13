namespace AccountService.Domain.Enums
{
    public enum CargoStatus : byte
    {
        Pending = 0,
        InTransit = 1,
        Delivered = 2,
        Cancelled = 3
    }

    public class CargoStatusInfo
    {
        public CargoStatus Value { get; set; }
        public required string Text { get; set; }
    }

    public class CargoStatuses
    {
        public CargoStatusInfo[] GetStatuses()
        {
            return
            [
                new CargoStatusInfo { Value = CargoStatus.Pending, Text = "Beklemede" },
                new CargoStatusInfo { Value = CargoStatus.InTransit, Text = "Yolda" },
                new CargoStatusInfo { Value = CargoStatus.Delivered, Text = "Teslim Edildi" },
                new CargoStatusInfo { Value = CargoStatus.Cancelled, Text = "İptal Edildi" }
            ];
        }

        public string GetName(CargoStatus value)
        {
            return GetStatuses().First(p => p.Value == value).Text;
        }
    }
} 