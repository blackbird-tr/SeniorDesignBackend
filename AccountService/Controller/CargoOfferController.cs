using Microsoft.AspNetCore.Mvc;
using AccountService.WebApi.Controller;
using AccountService.Application.Features.CargoOffer.Commands.Create;
using AccountService.Application.Features.CargoOffer.Commands.UpdateStatus;
using AccountService.Application.Features.CargoOffer.Queries.GetAll;
using AccountService.Application.Features.CargoOffer.Queries.GetById;
using AccountService.Application.Features.CargoOffer.Queries.GetBySenderId;
using AccountService.Application.Features.CargoOffer.Queries.GetByReceiverId;
using AccountService.Application.Features.CargoOffer.Queries.GetByCargoAdId;
using AccountService.Application.Features.CargoOffer.Queries.GetPending;
using System;
using System.Threading.Tasks;

namespace AccountService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargoOfferController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCargoOfferCommand command)
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
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateCargoOfferStatusCommand command)
        {
            if (id != command.OfferId)
                return BadRequest();

            var result = await Mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllCargoOffersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetCargoOfferByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("sender/{senderId}")]
        public async Task<IActionResult> GetBySenderId(string senderId)
        {
            var result = await Mediator.Send(new GetCargoOffersBySenderIdQuery { SenderId = senderId });
            return Ok(result);
        }

        [HttpGet("receiver/{receiverId}")]
        public async Task<IActionResult> GetByReceiverId(string receiverId)
        {
            var result = await Mediator.Send(new GetCargoOffersByReceiverIdQuery { ReceiverId = receiverId });
            return Ok(result);
        }

        [HttpGet("cargo-ad/{cargoAdId}")]
        public async Task<IActionResult> GetByCargoAdId(int cargoAdId)
        {
            var result = await Mediator.Send(new GetCargoOffersByCargoAdIdQuery { CargoAdId = cargoAdId });
            return Ok(result);
        }

        [HttpGet("pending/{userId}")]
        public async Task<IActionResult> GetPendingOffers(string userId)
        {
            var result = await Mediator.Send(new GetPendingCargoOffersQuery { UserId = userId });
            return Ok(result);
        }
    }
} 