using AccountService.Application.Common.Helpers;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Repositories
{
    public class CarrierRepositoryAsync : GenericRepository<Carrier>, ICarrierService
    {
        private readonly ApplicationDbContext _context;

        public CarrierRepositoryAsync(ApplicationDbContext application_context)
            : base(application_context)
        {
            _context = application_context;
        }

        public async Task<Carrier?> GetByUserIdAsync(string userId)
        {
            return await _context.Carriers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<bool> IsLicenseNumberUniqueAsync(string licenseNumber)
        {
            return !await _context.Carriers
                .AnyAsync(c => c.LicenseNumber == licenseNumber);
        }

        public async Task<List<Carrier>> GetAvailableCarriersAsync()
        {
            return await _context.Carriers
                .Where(c => c.AvailabilityStatus == true)
                .Include(c => c.User)
                .Include(c => c.Vehicles)
                .ToListAsync();
        }
    }

}
