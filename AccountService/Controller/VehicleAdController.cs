using Microsoft.AspNetCore.Mvc;
using AccountService.WebApi.Controller;
using AccountService.Application.Features.VehicleAd.Commands.Create;
using AccountService.Application.Features.VehicleAd.Commands.Update;
using AccountService.Application.Features.VehicleAd.Commands.Delete;
using AccountService.Application.Features.VehicleAd.Queries.GetAll;
using AccountService.Application.Features.VehicleAd.Queries.GetById;
using AccountService.Application.Features.VehicleAd.Queries.GetByCarrierId;
using AccountService.Application.Features.VehicleAd.Queries.GetByVehicleType;
using AccountService.Application.Features.VehicleAd.Queries.GetByPickUpLocation;

namespace AccountService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleAdController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllVehicleAdsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetVehicleAdByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleAdCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVehicleAdCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await Mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteVehicleAdCommand { Id = id });
            return result ? NoContent() : NotFound();
        }

        [HttpGet("by-carrier/{carrierId}")]
        public async Task<IActionResult> GetByCarrierId(string carrierId)
        {
            return Ok(await Mediator.Send(new GetVehicleAdsByCarrierIdQuery { CarrierId = carrierId }));
        }

        [HttpGet("by-type/{vehicleType}")]
        public async Task<IActionResult> GetByVehicleType(string vehicleType)
        {
            return Ok(await Mediator.Send(new GetVehicleAdsByVehicleTypeQuery { VehicleType = vehicleType }));
        }

        [HttpGet("by-location/{pickUpLocationId}")]
        public async Task<IActionResult> GetByPickUpLocation(int pickUpLocationId)
        {
            return Ok(await Mediator.Send(new GetVehicleAdsByPickUpLocationQuery { PickUpLocationId = pickUpLocationId }));
        }
    }
} 