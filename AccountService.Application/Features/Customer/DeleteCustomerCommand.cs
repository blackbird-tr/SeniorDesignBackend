using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application
{
    public class DeleteCustomerCommand : IRequest
    {
        public int CustomerId { get; set; }
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null) throw new Exception("Customer not found");

            await _customerRepository.DeleteAsync(customer);
            return Unit.Value;
        }
    }
}