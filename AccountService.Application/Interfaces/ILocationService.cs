using AccountService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface ILocationService:IGenericRepository<Domain.Entities.Location>
    {
        Task<Location?> GetByFullAddressAsync(string? address, string? city, string? state, int? postalCode);

         Task<Location?> GetByCoordinatesAsync(string coordinates);

         Task<List<Location>> GetByCityAsync(string city);

          Task<List<Location>> GetByPostalCodeAsync(int postalCode);

        Task<List<Location>> GetByRegionAsync(string city, string state);

    }
}
