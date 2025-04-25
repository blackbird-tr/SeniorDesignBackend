
using AccountService.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLocationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLocationCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("locationId")]
        public async Task<IActionResult> Delete(int locationId)
        {
            var command = new DeleteLocationCommand { LocationId = locationId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}