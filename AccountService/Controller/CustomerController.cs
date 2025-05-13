using Microsoft.AspNetCore.Mvc;
using AccountService.Application.Features.Customer.Commands.CreateCustomer;
using AccountService.Application.Features.Customer.Commands.UpdateCustomer;
using AccountService.Application.Features.Customer.Commands.DeleteCustomer;
using AccountService.Application.Features.Customer.Queries.GetAll;
using AccountService.Application.Features.Customer.Queries.GetById;
using AccountService.Application.Features.Customer.Queries.GetByUserId;
using AccountService.WebApi.Controller;
using MediatR;

namespace AccountService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCustomerCommand { CustomerId = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCustomersQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetCustomerByIdQuery { CustomerId = id }));
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            return Ok(await Mediator.Send(new GetCustomerByUserIdQuery { UserId = userId }));
        }
    }
} 