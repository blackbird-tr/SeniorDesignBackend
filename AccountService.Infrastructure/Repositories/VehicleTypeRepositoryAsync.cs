using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Repositories
{
    public class VehicleTypeRepositoryAsync : GenericRepository<VehicleType>, IVehicleTypeService
    {
        private readonly ApplicationDbContext _context;

        public VehicleTypeRepositoryAsync(ApplicationDbContext application_context)
            : base(application_context)
        {
            _context = application_context;
        }

        public async Task<List<VehicleType>> GetAllVehicleTypesAsync()
        {
            return await _context.VehicleTypes
                .Where(vt => vt.Active)
                .ToListAsync();
        }
    }
} 