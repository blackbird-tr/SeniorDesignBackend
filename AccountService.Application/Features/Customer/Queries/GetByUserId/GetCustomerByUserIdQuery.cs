using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Common.DTOs;

namespace AccountService.Application.Features.Customer.Queries.GetByUserId
{
    public class GetCustomerByUserIdQuery : IRequest<CustomerResponse>
    {
        public string UserId { get; set; }
    }

    public class GetCustomerByUserIdQueryHandler : IRequestHandler<GetCustomerByUserIdQuery, CustomerResponse>
    {
        private readonly ICustomerService _customerService;

        public GetCustomerByUserIdQueryHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<CustomerResponse> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetByUserIdAsync(request.UserId);
            if (customer == null)
                return null;

            return new CustomerResponse
            {
                CustomerId = customer.Id,
                UserId = customer.UserId,
                Address = customer.Address,
                UserName = customer.User?.UserName,
                Email = customer.User?.Email
            };
        }
    }
} 