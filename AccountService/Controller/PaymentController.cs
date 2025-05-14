using Microsoft.AspNetCore.Mvc;
using AccountService.WebApi.Controller;
using MediatR;
using AccountService.Application.Features.Payment.Commands.CreatePayment;
using AccountService.Application.Features.Payment.Commands.UpdatePayment;
using AccountService.Application.Features.Payment.Commands.DeletePayment;
using AccountService.Application.Features.Payment.Queries.GetAll;
using AccountService.Application.Features.Payment.Queries.GetById;
using AccountService.Application.Features.Payment.Queries.GetByBookingId;
using AccountService.Application.Features.Payment.Queries.GetByStatus;
using AccountService.Application.Features.Payment.Queries.GetByDateRange;
using AccountService.Application.Features.Payment.Queries.GetTotalByBooking;
using AccountService.Application.Features.Payment.Commands.ChangeStatus;

namespace AccountService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllPaymentsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetPaymentByIdQuery { Id = id });
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePaymentCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await Mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeletePaymentCommand { Id = id });
            return result ? NoContent() : NotFound();
        }

        [HttpGet("by-booking/{bookingId}")]
        public async Task<IActionResult> GetByBookingId(int bookingId)
        {
            return Ok(await Mediator.Send(new GetPaymentsByBookingIdQuery { BookingId = bookingId }));
        }

        [HttpGet("by-status/{status}")]
        public async Task<IActionResult> GetByStatus(byte status)
        {
            return Ok(await Mediator.Send(new GetPaymentsByStatusQuery { Status = status }));
        }

        [HttpGet("by-date")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            return Ok(await Mediator.Send(new GetPaymentsByDateRangeQuery
            {
                Start = start,
                End = end
            }));
        }
        [HttpPatch("{id}/status/{status}")]
        public async Task<IActionResult> ChangeStatus(int id, byte status)
        {
            var result = await Mediator.Send(new ChangePaymentStatusCommand
            {
                PaymentId = id,
                Status = status
            });

            return result ? NoContent() : NotFound();
        }

        [HttpGet("total-by-booking/{bookingId}")]
        public async Task<IActionResult> GetTotalPaidAmount(int bookingId)
        {
            var result = await Mediator.Send(new GetTotalPaidAmountByBookingIdQuery
            {
                BookingId = bookingId
            });

            return Ok(result ?? 0); // Eğer hiç ödeme yoksa 0 döndür
        }
    }
}
