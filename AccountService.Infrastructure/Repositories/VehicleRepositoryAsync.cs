using AccountService.Application.Interfaces;
using AccountService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Repositories
{
    public class VehicleRepositoryAsync : GenericRepository<AccountService.Domain.Entities.Vehicle>, IVehicleSerivce
    {
        public VehicleRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
