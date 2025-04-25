
using AccountService.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePaymentCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("paymentId")]
        public async Task<IActionResult> Delete(int paymentId)
        {
            var command = new DeletePaymentCommand { PaymentId = paymentId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}