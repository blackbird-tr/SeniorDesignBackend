
using AccountService.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNotificationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateNotificationCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("notificationId")]
        public async Task<IActionResult> Delete(int notificationId)
        {
            var command = new DeleteNotificationCommand { NotificationId = notificationId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}