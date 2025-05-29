using AccountService.Application.Common.Helpers;
using AccountService.Application.Interfaces;
using AccountService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Services
{
    public class AdminAuthService : IAdminAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly JWTSettings _jwtSettings;

        public AdminAuthService(ApplicationDbContext context, IConfiguration configuration, IOptions<JWTSettings> jwtSettings)
        {
            _context = context;
            _configuration = configuration;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<(string AdminId, string JwtToken, string RefreshToken)> AuthenticateAsync(string username, string password)
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Username == username && a.Password == password);

            if (admin == null)
                throw new Exception("Invalid username or password");

            var token = GenerateJwtToken(admin);
            var refreshToken = GenerateRefreshToken();

            //// Refresh token'ı kaydet
            //_context.RefreshTokens.Add(new Domain.Entities.RefreshToken
            //{
            //    UserID = admin.Id.ToString(),
            //    Token = refreshToken,
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    Created = DateTime.UtcNow,
            //    CreatedByIp = "AdminLogin"
            //});

            //await _context.SaveChangesAsync();

            return (admin.Id.ToString(), token, refreshToken);
        }

        private string GenerateJwtToken(Domain.Entities.Admin admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                    new Claim(ClaimTypes.Name, admin.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
} 