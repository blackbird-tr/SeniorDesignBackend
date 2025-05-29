using Microsoft.AspNetCore.Mvc;
using AccountService.WebApi.Controller;
using AccountService.Application.Features.CargoAd.Commands.Create;
using AccountService.Application.Features.CargoAd.Commands.Update;
using AccountService.Application.Features.CargoAd.Commands.Delete;
using AccountService.Application.Features.CargoAd.Queries.GetAll;
using AccountService.Application.Features.CargoAd.Queries.GetById;
using AccountService.Application.Features.CargoAd.Queries.GetByCustomerId;
using AccountService.Application.Features.CargoAd.Queries.GetByCargoType;
using AccountService.Application.Features.CargoAd.Queries.GetByPickCity;
using AccountService.Application.Features.CargoAd.Queries.GetByPickCountry;
using AccountService.Application.Features.CargoAd.Queries.GetByDropCity;
using AccountService.Application.Features.CargoAd.Queries.GetByDropCountry;
using AccountService.Application.Features.CargoAd.Commands.Accept;
using AccountService.Application.Features.CargoAd.Commands.Reject; 
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AccountService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargoAdController : BaseApiController
    { 

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] byte? status = null)
        {
            var query = new GetAllCargoAdsQuery { Status = status };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] byte? status = null)
        {
            var result = await Mediator.Send(new GetCargoAdByIdQuery { Id = id, Status = status });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCargoAdCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCargoAdCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await Mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteCargoAdCommand { Id = id });
            return result ? NoContent() : NotFound();
        }

        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(string customerId, [FromQuery] byte? status = null)
        {
            return Ok(await Mediator.Send(new GetCargoAdsByCustomerIdQuery { CustomerId = customerId, Status = status }));
        }

        [HttpGet("by-type/{cargoType}")]
        public async Task<IActionResult> GetByCargoType(string cargoType, [FromQuery] byte? status = null)
        {
            return Ok(await Mediator.Send(new GetCargoAdsByCargoTypeQuery { CargoType = cargoType, Status = status }));
        }

        [HttpGet("by-pick-city/{city}")]
        public async Task<IActionResult> GetByPickCity(string city, [FromQuery] byte? status = null)
        {
            return Ok(await Mediator.Send(new GetCargoAdsByPickCityQuery { City = city, Status = status }));
        }

        [HttpGet("by-pick-country/{country}")]
        public async Task<IActionResult> GetByPickCountry(string country, [FromQuery] byte? status = null)
        {
            return Ok(await Mediator.Send(new GetCargoAdsByPickCountryQuery { Country = country, Status = status }));
        }

        [HttpGet("by-drop-city/{city}")]
        public async Task<IActionResult> GetByDropCity(string city, [FromQuery] byte? status = null)
        {
            return Ok(await Mediator.Send(new GetCargoAdsByDropCityQuery { City = city, Status = status }));
        }

        [HttpGet("by-drop-country/{country}")]
        public async Task<IActionResult> GetByDropCountry(string country, [FromQuery] byte? status = null)
        {
            return Ok(await Mediator.Send(new GetCargoAdsByDropCountryQuery { Country = country, Status = status }));
        }
         
        
 
    }
} 