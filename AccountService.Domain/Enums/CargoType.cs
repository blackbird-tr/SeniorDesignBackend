namespace AccountService.Domain.Enums
{
    public enum CargoType
    {
        General = 1,           // TarpaulinTruck
        Fragile = 2,           // BoxTruck
        Refrigerated = 3,      // RefrigeratedTruck
        Oversized = 4,         // SemiTrailer
        LightFreight = 5,      // LightTruck
        Containerized = 6,     // ContainerCarrier
        Liquid = 7,            // TankTruck
        HeavyMachinery = 8,    // LowbedTrailer
        Construction = 9,      // DumpTruck
        Parcel = 10,           // PanelVan
        Others = 11            // Others
    }
} 