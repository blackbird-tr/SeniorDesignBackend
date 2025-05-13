using Microsoft.AspNetCore.Mvc;
using AccountService.WebApi.Controller;
using AccountService.Application.Features.VehicleType.Commands.CreateVehicleType;
using AccountService.Application.Features.VehicleType.Commands.DeleteVehicleType;
using AccountService.Application.Features.VehicleType.Commands.UpdateVehicleType;
using AccountService.Application.Features.VehicleType.Queries.GetAll;
using AccountService.Application.Features.VehicleType.Queries.GetById;
using AccountService.Application.Common.DTOs;

namespace AccountService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleTypeController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllVehicleTypesQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetVehicleTypeByIdQuery { VehicleTypeId = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleTypeCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVehicleTypeCommand command)
        {
            if (id != command.VehicleTypeId)
                return BadRequest();

            var result = await Mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteVehicleTypeCommand { VehicleTypeId = id });
            return result ? NoContent() : NotFound();
        }
    }
}
