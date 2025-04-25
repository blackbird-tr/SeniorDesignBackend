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
    public class LocationRepository :GenericRepository<Location>, ILocationRepository
{
    private readonly ApplicationDbContext _context;

    public LocationRepository(ApplicationDbContext context):base(context)
        {
        _context = context;
    }

        public Task<Location?> GetByCoordinatesAsync(string coordinates)
        {
            throw new NotImplementedException();
        }

        // TODO: Implement ILocationRepository methods
    }
}