namespace AccountService.Application.Features.VehicleAd.Queries.GetAll
{
    public class VehicleAdDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PickUpLocationId { get; set; }
        public string CarrierId { get; set; }
        public string CarrierName { get; set; }
        public string VehicleType { get; set; }
        public float Capacity { get; set; }
        public DateTime CreatedDate { get; set; }
    }
} 