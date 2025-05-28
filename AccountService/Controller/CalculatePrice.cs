using AccountService.Application.Features.CargoOffer.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.WebApi.Controller
{
    public class CalculatePrice : BaseApiController
    {
        //[HttpGet]
        //public async Task<IActionResult> Calculate(CalculateModel model)
        //{
          
        //}






        public class CalculateModel
        {
            public string CargoType { get; set; }
            public string PickCity { get; set; }
            public string PickCountry { get; set; }
            public string DeliveryCity { get; set; }
            public string DeliveryCountry { get; set; }
            public double Weight { get; set; }
        }
    }
    }
