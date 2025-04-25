
using AccountService.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeedbackController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFeedbackCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFeedbackCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("feedbackId")]
        public async Task<IActionResult> Delete(int feedbackId)
        {
            var command = new DeleteFeedbackCommand { FeedbackId = feedbackId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}