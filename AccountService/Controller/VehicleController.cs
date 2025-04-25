
using AccountService.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("vehicleId")]
        public async Task<IActionResult> Delete(int vehicleId)
        {
            var command = new DeleteVehicleCommand { VehicleId = vehicleId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}