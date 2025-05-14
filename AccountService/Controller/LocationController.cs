using Microsoft.AspNetCore.Mvc;
using AccountService.WebApi.Controller;
using AccountService.Application.Features.Location.Commands.CreateLocation;
using AccountService.Application.Features.Location.Commands.UpdateLocation;
using AccountService.Application.Features.Location.Commands.DeleteLocation;
using AccountService.Application.Features.Location.Queries.GetAll;
using AccountService.Application.Features.Location.Queries.GetById;
using AccountService.Application.Features.Location.Queries.GetByCity;
using AccountService.Application.Features.Location.Queries.GetByCoordinates;
using AccountService.Application.Features.Location.Queries.GetByFullAddress;
using AccountService.Application.Features.Location.Queries.GetByPostalCode;
using AccountService.Application.Features.Location.Queries.GetByRegion;

namespace AccountService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllLocationsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetLocationByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLocationCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLocationCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await Mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteLocationCommand { Id = id });
            return result ? NoContent() : NotFound();
        }

        [HttpGet("by-coordinates")]
        public async Task<IActionResult> GetByCoordinates([FromQuery] string coordinates)
        {
            var result = await Mediator.Send(new GetLocationByCoordinatesQuery { Coordinates = coordinates });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("by-address")]
        public async Task<IActionResult> GetByFullAddress(
            [FromQuery] string? address,
            [FromQuery] string? city,
            [FromQuery] string? state,
            [FromQuery] int? postalCode)
        {
            var result = await Mediator.Send(new GetLocationByFullAddressQuery
            {
                Address = address,
                City = city,
                State = state,
                PostalCode = postalCode
            });

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("by-city/{city}")]
        public async Task<IActionResult> GetByCity(string city)
        {
            return Ok(await Mediator.Send(new GetLocationsByCityQuery { City = city }));
        }

        [HttpGet("by-postalcode/{postalCode}")]
        public async Task<IActionResult> GetByPostalCode(int postalCode)
        {
            return Ok(await Mediator.Send(new GetLocationsByPostalCodeQuery { PostalCode = postalCode }));
        }

        [HttpGet("by-region")]
        public async Task<IActionResult> GetByRegion([FromQuery] string city, [FromQuery] string state)
        {
            return Ok(await Mediator.Send(new GetLocationsByRegionQuery { City = city, State = state }));
        }
    }
}
