using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PocDDD.Application.Interfaces;
using PocDDD.Application.Services;
using PocDDD.Domain.Interfaces;
using PocDDD.Infra.Data.Repositories;

namespace PocDDD.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInsfraStructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserRespository, UserRepository>();

            return services;
        }

    }
}