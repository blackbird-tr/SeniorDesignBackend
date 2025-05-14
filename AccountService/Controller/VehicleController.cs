using Microsoft.AspNetCore.Mvc;
using AccountService.WebApi.Controller;
using AccountService.Application.Features.Vehicle.Commands.CreateVehicle;
using AccountService.Application.Features.Vehicle.Commands.UpdateVehicle;
using AccountService.Application.Features.Vehicle.Commands.DeleteVehicle;
using AccountService.Application.Features.Vehicle.Queries.GetAll;
using AccountService.Application.Features.Vehicle.Queries.GetById;
using AccountService.Application.Features.Vehicle.Queries.GetAvailable;
using AccountService.Application.Features.Vehicle.Queries.GetByCarrierId;
using AccountService.Application.Features.Vehicle.Queries.GetByLicensePlate;
using AccountService.Application.Features.Vehicle.Queries.GetByVehicleTypeId;

namespace AccountService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllVehiclesQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetVehicleByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVehicleCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await Mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteVehicleCommand { Id = id });
            return result ? NoContent() : NotFound();
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable()
        {
            return Ok(await Mediator.Send(new GetAvailableVehiclesQuery()));
        }

        [HttpGet("by-carrier/{carrierId}")]
        public async Task<IActionResult> GetByCarrierId(int carrierId)
        {
            return Ok(await Mediator.Send(new GetVehiclesByCarrierIdQuery { CarrierId = carrierId }));
        }

        [HttpGet("by-license/{licensePlate}")]
        public async Task<IActionResult> GetByLicensePlate(string licensePlate)
        {
            var result = await Mediator.Send(new GetVehicleByLicensePlateQuery { LicensePlate = licensePlate });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("by-type/{vehicleTypeId}")]
        public async Task<IActionResult> GetByVehicleTypeId(int vehicleTypeId)
        {
            return Ok(await Mediator.Send(new GetVehiclesByVehicleTypeIdQuery { VehicleTypeId = vehicleTypeId }));
        }
    }
}
