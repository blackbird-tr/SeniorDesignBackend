
namespace AccountService.Application.Common.DTOs 
{
    public class BookingResponse
{
    public int BookingId { get; set; }
    public int CustomerId { get; set; }
    public int CarrierId { get; set; }
    public int VehicleId { get; set; }
    public int CargoId { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime DropoffDate { get; set; }
    public double TotalPrice { get; set; }
    public string Status { get; set; }
    public bool IsFuelIncluded { get; set; }
    }
}