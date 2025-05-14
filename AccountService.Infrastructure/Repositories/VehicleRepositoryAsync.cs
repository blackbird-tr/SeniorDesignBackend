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
    public class VehicleRepositoryAsync : GenericRepository<Vehicle>, IVehicleSerivce
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Vehicle>> GetByCarrierIdAsync(int carrierId)
        {
            return await _context.Vehicles
                .Where(v => v.CarrierId == carrierId && v.Active)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetAvailableVehiclesAsync()
        {
            return await _context.Vehicles
                .Where(v => v.AvailabilityStatus && v.Active)
                .ToListAsync();
        }

        public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate)
        {
            return await _context.Vehicles
                .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate && v.Active);
        }

        public async Task<List<Vehicle>> GetByVehicleTypeIdAsync(int vehicleTypeId)
        {
            return await _context.Vehicles
                .Where(v => v.VehicleTypeId == vehicleTypeId && v.Active)
                .ToListAsync();
        }
    }
}