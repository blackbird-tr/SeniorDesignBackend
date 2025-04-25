
namespace AccountService.Application.Common.DTOs
{
    public class LocationResponse
{
    public int LocationId { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int PostalCode { get; set; }
    public string Coordinates { get; set; }
}
}