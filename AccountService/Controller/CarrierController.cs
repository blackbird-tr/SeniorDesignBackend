using Microsoft.AspNetCore.Mvc;
using AccountService.Application.Features.Carrier.Commands.CreateCarrier;
using AccountService.Application.Features.Carrier.Commands.UpdateCarrier;
using AccountService.Application.Features.Carrier.Commands.DeleteCarrier;
using AccountService.Application.Features.Carrier.Queries.GetAll;
using AccountService.Application.Features.Carrier.Queries.GetById;
using AccountService.Application.Features.Carrier.Queries.GetByUserId;
using AccountService.Application.Features.Carrier.Queries.GetAvailable;
using AccountService.WebApi.Controller;
using MediatR;

namespace AccountService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarrierCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCarrierCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCarrierCommand { CarrierId = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCarriersQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetCarrierByIdQuery { CarrierId = id }));
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            return Ok(await Mediator.Send(new GetCarrierByUserIdQuery { UserId = userId }));
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable()
        {
            return Ok(await Mediator.Send(new GetAvailableCarriersQuery()));
        }
    }
}
