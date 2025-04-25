
using AccountService.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBookingCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("bookingId")]
        public async Task<IActionResult> Delete(int bookingId)
        {
            var command = new DeleteBookingCommand { BookingId = bookingId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}