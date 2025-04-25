
using AccountService.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarrierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarrierCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCarrierCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("carrierId")]
        public async Task<IActionResult> Delete(int carrierId)
        {
            var command = new DeleteCarrierCommand { CarrierId = carrierId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}