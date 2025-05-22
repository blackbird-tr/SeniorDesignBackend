using Microsoft.AspNetCore.Mvc;
using AccountService.WebApi.Controller;
using AccountService.Application.Features.VehicleOffer.Commands.Create;
using AccountService.Application.Features.VehicleOffer.Commands.UpdateStatus;
using AccountService.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace AccountService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleOfferController : BaseApiController
    {
        private readonly IVehicleOfferService _vehicleOfferService;

        public VehicleOfferController(IVehicleOfferService vehicleOfferService)
        {
            _vehicleOfferService = vehicleOfferService;
        }

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
            var offers = await _vehicleOfferService.GetAllAsync();
            return Ok(offers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var offer = await _vehicleOfferService.GetByIdAsync(id);
            if (offer == null)
                return NotFound();

            return Ok(offer);
        }

        [HttpGet("sender/{senderId}")]
        public async Task<IActionResult> GetBySenderId(string senderId)
        {
            var offers = await _vehicleOfferService.GetBySenderIdAsync(senderId);
            return Ok(offers);
        }

        [HttpGet("receiver/{receiverId}")]
        public async Task<IActionResult> GetByReceiverId(string receiverId)
        {
            var offers = await _vehicleOfferService.GetByReceiverIdAsync(receiverId);
            return Ok(offers);
        }

        [HttpGet("vehicle-ad/{vehicleAdId}")]
        public async Task<IActionResult> GetByVehicleAdId(int vehicleAdId)
        {
            var offers = await _vehicleOfferService.GetByVehicleAdIdAsync(vehicleAdId);
            return Ok(offers);
        }

        [HttpGet("pending/{userId}")]
        public async Task<IActionResult> GetPendingOffers(string userId)
        {
            var offers = await _vehicleOfferService.GetPendingOffersAsync(userId);
            return Ok(offers);
        }
    }
} 