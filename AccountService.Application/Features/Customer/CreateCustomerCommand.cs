using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using MediatR;

namespace AccountService.Application
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public string Address { get; set; }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Address = request.Address
            };

            await _customerRepository.AddAsync(customer);
            return customer.CustomerId;
        }
    }
}