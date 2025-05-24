using Microsoft.AspNetCore.Mvc;
using AccountService.WebApi.Controller;
using AccountService.Application.Features.VehicleOffer.Commands.Create;
using AccountService.Application.Features.VehicleOffer.Commands.UpdateStatus;
using AccountService.Application.Features.VehicleOffer.Queries.GetAll;
using AccountService.Application.Features.VehicleOffer.Queries.GetById;
using AccountService.Application.Features.VehicleOffer.Queries.GetBySenderId;
using AccountService.Application.Features.VehicleOffer.Queries.GetByReceiverId;
using AccountService.Application.Features.VehicleOffer.Queries.GetByVehicleAdId;
using AccountService.Application.Features.VehicleOffer.Queries.GetPending;
using System;
using System.Threading.Tasks;

namespace AccountService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleOfferController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleOfferCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Success = false, Error = ex.Message });
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateVehicleOfferStatusCommand command)
        {
            if (id != command.OfferId)
                return BadRequest();

            var result = await Mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllVehicleOffersQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetVehicleOfferByIdQuery { Id = id };
            var result = await Mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("sender/{senderId}")]
        public async Task<IActionResult> GetBySenderId(string senderId)
        {
            var query = new GetVehicleOffersBySenderIdQuery { SenderId = senderId };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("receiver/{receiverId}")]
        public async Task<IActionResult> GetByReceiverId(string receiverId)
        {
            var query = new GetVehicleOffersByReceiverIdQuery { ReceiverId = receiverId };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("vehicle-ad/{vehicleAdId}")]
        public async Task<IActionResult> GetByVehicleAdId(int vehicleAdId)
        {
            var query = new GetVehicleOffersByVehicleAdIdQuery { VehicleAdId = vehicleAdId };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("pending/{userId}")]
        public async Task<IActionResult> GetPendingOffers(string userId)
        {
            var query = new GetPendingVehicleOffersQuery { UserId = userId };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
} 