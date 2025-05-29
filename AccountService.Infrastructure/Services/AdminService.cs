using AccountService.Application.Interfaces;
using AccountService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;

        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string adminId)
        {
            try
            {
                var guid = Guid.Parse(adminId);
                return await _context.Admins.AnyAsync(a => a.Id == guid);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
} 