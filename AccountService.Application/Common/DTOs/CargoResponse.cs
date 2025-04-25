
namespace AccountService.Application.Common.DTOs
{
    public class CargoResponse
{
    public int CargoId { get; set; }
    public int CustomerId { get; set; }
    public string Desc { get; set; }
    public double Weight { get; set; }
    public string CargoType { get; set; }
    public string PickUpLocation { get; set; }
    public string DropOffLocation { get; set; }
    public string Status { get; set; }
}
}