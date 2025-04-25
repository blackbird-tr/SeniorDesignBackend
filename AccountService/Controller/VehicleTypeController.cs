
using AccountService.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleTypeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleTypeCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("vehicletypeId")]
        public async Task<IActionResult> Delete(int vehicletypeId)
        {
            var command = new DeleteVehicleTypeCommand { VehicleTypeId = vehicletypeId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}