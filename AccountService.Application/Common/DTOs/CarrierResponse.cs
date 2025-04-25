
namespace AccountService.Application.Common.DTOs
{
    public class CarrierResponse
{
    public int CarrierId { get; set; }
    public int VehicleTypeId { get; set; }
    public string LicenseNumber { get; set; }
    public bool AvailabilityStatus { get; set; }
}
}