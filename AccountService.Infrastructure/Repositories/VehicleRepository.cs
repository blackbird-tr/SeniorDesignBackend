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
    public class VehicleRepository :GenericRepository<Vehicle>, IVehicleRepository
{
    private readonly ApplicationDbContext _context;

    public VehicleRepository(ApplicationDbContext context):base(context)
        {
        _context = context;
    }

        public Task<IReadOnlyList<Vehicle>> GetAvailableVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle?> GetVehicleWithTypeAsync(int vehicleId)
        {
            throw new NotImplementedException();
        }

        // TODO: Implement IVehicleRepository methods
    }
}