using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context):base(context)
    {
        _context = context;
    }

        public Task<IReadOnlyList<Customer>> GetCustomersWithBookingsAsync()
        {
            throw new NotImplementedException();
        }

        // TODO: Implement ICustomerRepository methods
    }
}