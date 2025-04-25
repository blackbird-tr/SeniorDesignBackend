
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountService.Domain.Entities;

namespace AccountService.Application.Interfaces
{
    public interface ICarrierRepository : IGenericRepository<Carrier>

{
    Task<IReadOnlyList<Carrier>> GetAvailableCarriersAsync();
}
}