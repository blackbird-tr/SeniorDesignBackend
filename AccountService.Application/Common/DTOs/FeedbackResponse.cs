
namespace AccountService.Application.Common.DTOs
{
    public class FeedbackResponse
{
    public int FeedbackId { get; set; }
    public int BookingId { get; set; }
    public int UserId { get; set; }
    public double Rating { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
}
}