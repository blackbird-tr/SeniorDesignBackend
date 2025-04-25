
using AccountService.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CargoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCargoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCargoCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("cargoId")]
        public async Task<IActionResult> Delete(int cargoId)
        {
            var command = new DeleteCargoCommand { CargoId = cargoId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}