using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application
{
    public class UpdateCustomerCommand : IRequest
    {
        public int CustomerId { get; set; }
        public string Address { get; set; }
    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null) throw new Exception("Customer not found");

            customer.Address = request.Address;

            await _customerRepository.UpdateAsync(customer);
            return Unit.Value;
        }
    }
}