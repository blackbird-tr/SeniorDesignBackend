﻿using AccountService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface ICarrierService:IGenericRepository<Carrier>
    {
        Task<Carrier?> GetByUserIdAsync(string userId);
        Task<bool> IsLicenseNumberUniqueAsync(string licenseNumber);
        Task<List<Carrier>> GetAvailableCarriersAsync();
    }
}
