using AccountService.Application.Features.CargoAds.Commands;
using AccountService.Application.Features.CargoAds.Queries;
using AccountService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargoAdsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CargoAdsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CargoAd>>> GetAll()
        {
            var query = new GetAllCargoAdsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CargoAd>> GetById(int id)
        {
            var query = new GetCargoAdByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CargoAd>> Create(CreateCargoAdCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CargoAd>> Update(int id, UpdateCargoAdCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteCargoAdCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
} 