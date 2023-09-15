using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PocDDD.Application.Interfaces;
using PocDDD.Application.Services;
using PocDDD.Domain.Interfaces;
using PocDDD.Infra.Data.Context;
using PocDDD.Infra.Data.Repositories;
using PocDDD.Infra.Data.UnitOfWork;

namespace PocDDD.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInsfraStructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IUserRespository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddDataBase(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<AppDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return serviceCollection;
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}