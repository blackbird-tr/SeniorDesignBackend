using Microsoft.AspNetCore.Mvc;
using AccountService.WebApi.Controller;
using AccountService.Application.Features.CargoOffer.Commands.Create;
using AccountService.Application.Features.CargoOffer.Commands.UpdateStatus;
using AccountService.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace AccountService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargoOfferController : BaseApiController
    {
        private readonly ICargoOfferService _cargoOfferService;

        public CargoOfferController(ICargoOfferService cargoOfferService)
        {
            _cargoOfferService = cargoOfferService;
        }

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
            var offers = await _cargoOfferService.GetAllAsync();
            return Ok(offers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var offer = await _cargoOfferService.GetByIdAsync(id);
            if (offer == null)
                return NotFound();

            return Ok(offer);
        }

        [HttpGet("sender/{senderId}")]
        public async Task<IActionResult> GetBySenderId(string senderId)
        {
            var offers = await _cargoOfferService.GetBySenderIdAsync(senderId);
            return Ok(offers);
        }

        [HttpGet("receiver/{receiverId}")]
        public async Task<IActionResult> GetByReceiverId(string receiverId)
        {
            var offers = await _cargoOfferService.GetByReceiverIdAsync(receiverId);
            return Ok(offers);
        }

        [HttpGet("cargo-ad/{cargoAdId}")]
        public async Task<IActionResult> GetByCargoAdId(int cargoAdId)
        {
            var offers = await _cargoOfferService.GetByCargoAdIdAsync(cargoAdId);
            return Ok(offers);
        }

        [HttpGet("pending/{userId}")]
        public async Task<IActionResult> GetPendingOffers(string userId)
        {
            var offers = await _cargoOfferService.GetPendingOffersAsync(userId);
            return Ok(offers);
        }
    }
} 