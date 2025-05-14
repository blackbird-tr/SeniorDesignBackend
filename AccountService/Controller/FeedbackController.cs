using Microsoft.AspNetCore.Mvc;
using AccountService.WebApi.Controller;
using MediatR;
using AccountService.Application.Features.Feedback.Commands.CreateFeedback;
using AccountService.Application.Features.Feedback.Commands.UpdateFeedback;
using AccountService.Application.Features.Feedback.Commands.DeleteFeedback;
using AccountService.Application.Features.Feedback.Queries.GetAll;
using AccountService.Application.Features.Feedback.Queries.GetById;
using AccountService.Application.Features.Feedback.Queries.GetByBookingId;
using AccountService.Application.Features.Feedback.Queries.GetByUserId;
using AccountService.Application.Features.Feedback.Queries.GetAverageRating;

namespace AccountService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllFeedbacksQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetFeedbackByIdQuery { Id = id });
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFeedbackCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFeedbackCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await Mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteFeedbackCommand { Id = id });
            return result ? NoContent() : NotFound();
        }

        [HttpGet("by-booking/{bookingId}")]
        public async Task<IActionResult> GetByBookingId(int bookingId)
        {
            return Ok(await Mediator.Send(new GetFeedbacksByBookingIdQuery { BookingId = bookingId }));
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            return Ok(await Mediator.Send(new GetFeedbacksByUserIdQuery { UserId = userId }));
        }

        [HttpGet("average-rating/{bookingId}")]
        public async Task<IActionResult> GetAverageRating(int bookingId)
        {
            var result = await Mediator.Send(new GetAverageRatingByBookingIdQuery { BookingId = bookingId });
            return Ok(result ?? 0);
        }
    }
}
