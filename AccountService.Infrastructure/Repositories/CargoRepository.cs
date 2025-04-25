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
    public class CargoRepository : GenericRepository<Cargo>, ICargoRepository
{
    private readonly ApplicationDbContext _context;

    public CargoRepository(ApplicationDbContext context):base(context)
    {
        _context = context;
    }

        public Task<IReadOnlyList<Cargo>> GetCargosByCustomerIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}