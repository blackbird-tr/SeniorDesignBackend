using MediatR;
using Microsoft.AspNetCore.Identity;
using AccountService.Domain.Entities;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<CreateCustomerResponse>
    {
        public string UserId { get; set; }
        public string Address { get; set; }
    }

    public class CreateCustomerResponse
    {
        public int CustomerId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; } = "Customer created successfully.";
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICustomerService _customerService;

        public CreateCustomerCommandHandler(UserManager<User> userManager, ICustomerService customerService)
        {
            _userManager = userManager;
            _customerService = customerService;
        }

        public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var customer = new AccountService.Domain.Entities.Customer  
            {
                UserId = user.Id,
                Address = request.Address
            };

            var createdCustomer = await _customerService.AddAsync(customer);

            return new CreateCustomerResponse
            {
                CustomerId = createdCustomer.Id,
                UserId = user.Id
            };
        }
    }
} 