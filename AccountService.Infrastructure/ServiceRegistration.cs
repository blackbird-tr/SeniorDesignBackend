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


        public static void InfraPersistence(this IServiceCollection services,IConfiguration configuration)
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
              .AddJwtBearer(o =>
              {
                  o.RequireHttpsMetadata = false;
                  o.SaveToken = false;
                  o.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ClockSkew = TimeSpan.Zero,
                      ValidIssuer = configuration["JWTSettings:Issuer"],
                      ValidAudience = configuration["JWTSettings:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                  };
                  o.Events = new JwtBearerEvents()
                  {
                      OnAuthenticationFailed = c =>
                      {
                          c.NoResult();
                          c.Response.StatusCode = 500;
                          c.Response.ContentType = "text/plain";
                          return c.Response.WriteAsync(c.Exception.ToString());
                      },
                      OnChallenge = context =>
                      {
                          context.HandleResponse();
                          context.Response.StatusCode = 401;
                          context.Response.ContentType = "application/json";
                          var result = JsonConvert.SerializeObject(new AccountService.Application.Wrappers.Response<string>("You are not Authorized"));
                          return context.Response.WriteAsync(result);
                      },
                      OnForbidden = context =>
                      {
                          context.Response.StatusCode = 403;
                          context.Response.ContentType = "application/json";
                          var result = JsonConvert.SerializeObject(new AccountService.Application.Wrappers.Response<string>("You are not authorized to access this resource"));
                          return context.Response.WriteAsync(result);
                      },
                  };
              });

            services.AddSingleton<JwtSecurityTokenHandler>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICarrierService, CarrierRepositoryAsync>();
            services.AddTransient<ICustomerService, CustomerRepositoryAsync>();
            services.AddTransient<IVehicleTypeService, VehicleTypeRepositoryAsync>();
            services.AddTransient<IVehicleSerivce, VehicleRepositoryAsync>();
            services.AddTransient<ILocationService, LocationRepositoryAsync>();
            services.AddTransient<ICargoService, CargoRepositoryAsync>();
            
            services.AddTransient<IDateTimeService, DateTimeService>();

        }
    }
}
