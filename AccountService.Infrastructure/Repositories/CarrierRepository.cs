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
    public class CarrierRepository : GenericRepository<Carrier>, ICarrierRepository
{
    private readonly ApplicationDbContext _context;

    public CarrierRepository(ApplicationDbContext context):base(context)
        {
        _context = context;
    }

        public Task<IReadOnlyList<Carrier>> GetAvailableCarriersAsync()
        {
            throw new NotImplementedException();
        }

        // TODO: Implement ICarrierRepository methods
    }
}