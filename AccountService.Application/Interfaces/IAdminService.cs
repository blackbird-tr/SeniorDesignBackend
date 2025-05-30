using AccountService.Domain.Entities;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface IAdminService
    {
        Task<bool> ExistsAsync(string adminId);
    }
} 