using AccountService.Domain.Entities;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface ICustomerService : IGenericRepository<Customer>
    {
        Task<Customer?> GetByUserIdAsync(string userId);
        Task<List<Customer>> GetAllCustomersAsync();
    }
} 