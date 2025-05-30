namespace AccountService.Application.Features.VehicleAd.Queries.GetAll
{
    public class VehicleAdDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public string CarrierId { get; set; }
        public string CarrierName { get; set; }
        public string VehicleType { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public float Capacity { get; set; }
        public string Admin1Id { get; set; }
        public string Admin2Id { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime AdDate { get; set; }
    }
} 