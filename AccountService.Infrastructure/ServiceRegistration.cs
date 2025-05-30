using AccountService.Application.Common.Helpers;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using AccountService.Infrastructure.Repositories;
using AccountService.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AccountService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void InfraPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWTSettings:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    ClockSkew = System.TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return System.Threading.Tasks.Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = System.Text.Json.JsonSerializer.Serialize(new { message = "Unauthorized" });
                        return context.Response.WriteAsync(result);
                    },
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notificationHub"))
                        {
                            context.Token = accessToken;
                        }
                        return System.Threading.Tasks.Task.CompletedTask;
                    }
                };
            });

            services.AddSingleton<JwtSecurityTokenHandler>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAdminAuthService, AdminAuthService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IVehicleSerivce, VehicleRepositoryAsync>();
            services.AddTransient<IVehicleAdService, VehicleAdRepositoryAsync>();
            services.AddTransient<ICargoAdService, CargoAdRepository>();
            services.AddTransient<ICargoOfferService, CargoOfferRepository>();
            services.AddTransient<IVehicleOfferService, VehicleOfferRepository>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}
