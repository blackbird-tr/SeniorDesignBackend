using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Repositories
{
    public class CustomerRepositoryAsync : GenericRepository<Customer>, ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepositoryAsync(ApplicationDbContext application_context)
            : base(application_context)
        {
            _context = application_context;
        }

        public async Task<Customer?> GetByUserIdAsync(string userId)
        {
            return await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

       

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers
                .Include(c => c.User)
                .ToListAsync();
        }
    }
} 