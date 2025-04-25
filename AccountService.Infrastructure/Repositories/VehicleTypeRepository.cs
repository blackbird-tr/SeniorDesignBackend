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
    public class VehicleTypeRepository :GenericRepository<VehicleType>, IVehicleTypeRepository
{
    private readonly ApplicationDbContext _context;

    public VehicleTypeRepository(ApplicationDbContext context): base(context)
        {
        _context = context;
    }

        // TODO: Implement IVehicleTypeRepository methods
    }
}