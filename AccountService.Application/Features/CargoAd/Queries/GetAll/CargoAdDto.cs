using System;

namespace AccountService.Application.Features.CargoAd 
{
    public class CargoAdDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CustomerName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DropCountry { get; set; }
        public string DropCity { get; set; }
        public string PickCountry { get; set; }
        public string PickCity { get; set; }
        public string currency { get; set; }
        public float? Weight { get; set; }
        public string CargoType { get; set; }   
        public decimal Price { get; set; }
        public bool IsExpired { get; set; }
        public DateTime CreatedDate { get; set; }
    }
} 