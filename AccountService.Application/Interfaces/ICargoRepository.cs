
using AccountService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface ICargoRepository : IGenericRepository<Cargo>
{
    Task<IReadOnlyList<Cargo>> GetCargosByCustomerIdAsync(int customerId);
}
}