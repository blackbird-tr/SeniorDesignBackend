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

        public async Task<List<Vehicle>> GetByCarrierIdAsync(string UserId)
        {
            return await _context.Vehicles
                .Where(v => v.userId == UserId && v.Active)
                .ToListAsync();
        }

        

        public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate)
        {
            return await _context.Vehicles
                .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate && v.Active);
        }

     
    }
}