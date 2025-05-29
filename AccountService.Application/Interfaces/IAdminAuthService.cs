using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface IAdminAuthService
    {
        Task<(string AdminId, string JwtToken, string RefreshToken)> AuthenticateAsync(string username, string password);
    }
} 