using MediatR;
using AccountService.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using AccountService.Domain.Entities;
using AccountService.Application.Common.DTOs;

namespace AccountService.Application.Features.Customer.Queries.GetAll
{
    public class GetAllCustomersQuery : IRequest<List<CustomerResponse>> { }

    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerResponse>>
    {
        private readonly ICustomerService _customerService;
        private readonly UserManager<User> _userManager;

        public GetAllCustomersQueryHandler(ICustomerService customerService, UserManager<User> userManager)
        {
            _customerService = customerService;
            _userManager = userManager;
        }

        public async Task<List<CustomerResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerService.GetAllCustomersAsync();
            var dtoList = new List<CustomerResponse>();

            foreach (var customer in customers)
            {
                var user = await _userManager.FindByIdAsync(customer.UserId);

                dtoList.Add(new CustomerResponse
                {
                    CustomerId = customer.Id,
                    UserId = customer.UserId,
                    Address = customer.Address,
                    UserName = user?.UserName,
                    Email = user?.Email
                });
            }

            return dtoList;
        }
    }
} 