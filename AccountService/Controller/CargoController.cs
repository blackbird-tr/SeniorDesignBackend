using Microsoft.AspNetCore.Mvc;
using AccountService.WebApi.Controller;
using AccountService.Application.Features.Cargo.Commands.CreateCargo;
using AccountService.Application.Features.Cargo.Commands.UpdateCargo;
using AccountService.Application.Features.Cargo.Commands.DeleteCargo;
using AccountService.Application.Features.Cargo.Queries.GetAll;
using AccountService.Application.Features.Cargo.Queries.GetById;
using AccountService.Application.Features.Cargo.Queries.GetByCustomerId;
using AccountService.Application.Features.Cargo.Queries.GetByStatus;
using AccountService.Application.Features.Cargo.Queries.GetByPickupLocation;
using AccountService.Application.Features.Cargo.Queries.GetByDropoffLocation;
using AccountService.Application.Features.Cargo.Queries.GetByCargoType;
using AccountService.Application.Features.Cargo.Queries.GetByWeightRange;
using AccountService.Application.Features.Cargo.Commands.ChangeStatus;

namespace AccountService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargoController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCargosQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetCargoByIdQuery { Id = id });
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCargoCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCargoCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await Mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteCargoCommand { Id = id });
            return result ? NoContent() : NotFound();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] byte status)
        {
            var result = await Mediator.Send(new ChangeCargoStatusCommand
            {
                CargoId = id,
                Status = status
            });

            return result ? NoContent() : NotFound();
        }


        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(int customerId)
        {
            return Ok(await Mediator.Send(new GetCargosByCustomerIdQuery { CustomerId = customerId }));
        }

        [HttpGet("by-status/{status}")]
        public async Task<IActionResult> GetByStatus(byte status)
        {
            return Ok(await Mediator.Send(new GetCargosByStatusQuery { Status = status }));
        }

        [HttpGet("by-pickup/{pickupLocationId}")]
        public async Task<IActionResult> GetByPickupLocation(int pickupLocationId)
        {
            return Ok(await Mediator.Send(new GetCargosByPickupLocationIdQuery { PickupLocationId = pickupLocationId }));
        }

        [HttpGet("by-dropoff/{dropoffLocationId}")]
        public async Task<IActionResult> GetByDropoffLocation(int dropoffLocationId)
        {
            return Ok(await Mediator.Send(new GetCargosByDropoffLocationIdQuery { DropoffLocationId = dropoffLocationId }));
        }

        [HttpGet("by-type/{cargoType}")]
        public async Task<IActionResult> GetByCargoType(string cargoType)
        {
            return Ok(await Mediator.Send(new GetCargosByCargoTypeQuery { CargoType = cargoType }));
        }

        [HttpGet("by-weight")]
        public async Task<IActionResult> GetByWeightRange([FromQuery] float minWeight, [FromQuery] float maxWeight)
        {
            return Ok(await Mediator.Send(new GetCargosByWeightRangeQuery
            {
                MinWeight = minWeight,
                MaxWeight = maxWeight
            }));
        }
    }
}
