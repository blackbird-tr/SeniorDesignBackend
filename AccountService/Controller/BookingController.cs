using Microsoft.AspNetCore.Mvc;
using AccountService.WebApi.Controller;
using AccountService.Application.Features.Booking.Commands.CreateBooking;
using AccountService.Application.Features.Booking.Commands.UpdateBooking;
using AccountService.Application.Features.Booking.Commands.DeleteBooking;
using AccountService.Application.Features.Booking.Queries.GetAll;
using AccountService.Application.Features.Booking.Queries.GetById;
using AccountService.Application.Features.Booking.Queries.GetByCustomerId;
using AccountService.Application.Features.Booking.Queries.GetByCarrierId;
using AccountService.Application.Features.Booking.Queries.GetByVehicleId;
using AccountService.Application.Features.Booking.Queries.GetByCargoId;
using AccountService.Application.Features.Booking.Queries.GetByStatus;
using AccountService.Application.Features.Booking.Queries.GetByPickupDateRange;
using AccountService.Application.Features.Booking.Commands.ChangeStatus;

namespace AccountService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllBookingsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetBookingByIdQuery { Id = id });
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookingCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await Mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteBookingCommand { Id = id });
            return result ? NoContent() : NotFound();
        }

        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(int customerId)
        {
            return Ok(await Mediator.Send(new GetBookingsByCustomerIdQuery { CustomerId = customerId }));
        }

        [HttpGet("by-carrier/{carrierId}")]
        public async Task<IActionResult> GetByCarrierId(int carrierId)
        {
            return Ok(await Mediator.Send(new GetBookingsByCarrierIdQuery { CarrierId = carrierId }));
        }

        [HttpGet("by-vehicle/{vehicleId}")]
        public async Task<IActionResult> GetByVehicleId(int vehicleId)
        {
            return Ok(await Mediator.Send(new GetBookingsByVehicleIdQuery { VehicleId = vehicleId }));
        }

        [HttpGet("by-cargo/{cargoId}")]
        public async Task<IActionResult> GetByCargoId(int cargoId)
        {
            return Ok(await Mediator.Send(new GetBookingsByCargoIdQuery { CargoId = cargoId }));
        }

        [HttpGet("by-status/{status}")]
        public async Task<IActionResult> GetByStatus(byte status)
        {
            return Ok(await Mediator.Send(new GetBookingsByStatusQuery { Status = status }));
        }
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(int id, byte status)
        {
            var result = await Mediator.Send(new ChangeBookingStatusCommand
            {
                BookingId = id,
                Status = status
            });

            return result ? NoContent() : NotFound();
        }
        [HttpGet("by-pickupdate")]
        public async Task<IActionResult> GetByPickupDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            return Ok(await Mediator.Send(new GetBookingsByPickupDateRangeQuery
            {
                Start = start,
                End = end
            }));
        }
    }
}
