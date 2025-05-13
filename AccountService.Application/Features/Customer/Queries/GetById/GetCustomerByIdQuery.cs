using MediatR;
using Microsoft.AspNetCore.Identity;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Application.Common.DTOs;

namespace AccountService.Application.Features.Customer.Queries.GetById
{
    public class GetCustomerByIdQuery : IRequest<CustomerResponse>
    {
        public int CustomerId { get; set; }
    }

    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerResponse>
    {
        private readonly ICustomerService _customerService;
        private readonly UserManager<User> _userManager;

        public GetCustomerByIdQueryHandler(ICustomerService customerService, UserManager<User> userManager)
        {
            _customerService = customerService;
            _userManager = userManager;
        }

        public async Task<CustomerResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetByIdAsync(request.CustomerId);
            if (customer == null) return null;

            var user = await _userManager.FindByIdAsync(customer.UserId);

            return new CustomerResponse
            {
                CustomerId = customer.Id,
                UserId = customer.UserId,
                Address = customer.Address,
                UserName = user?.UserName,
                Email = user?.Email
            };
        }
    }
} 