
namespace AccountService.Application.Common.DTOs
{
    public class VehicleResponse
{
    public int VehicleId { get; set; }
    public int CarrierId { get; set; }
    public int VehicleTypeId { get; set; }
    public double Capacity { get; set; }
    public string LicensePlate { get; set; }
    public bool AvailabilityStatus { get; set; }
    public string Model { get; set; }
}
}