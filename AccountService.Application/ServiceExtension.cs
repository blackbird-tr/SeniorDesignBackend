using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AccountService.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
