
using AccountService.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("customerId")]
        public async Task<IActionResult> Delete(int customerId)
        {
            var command = new DeleteCustomerCommand { CustomerId = customerId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}