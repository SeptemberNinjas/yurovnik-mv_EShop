using Application.Orders;
using Application.SaleItem;
using DAL;
using DAL.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection RegisterApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<RepositoryFactory>(_ => new DatabaseRepositoryFactory(configuration["ConnectionString"] ?? ""))
                .AddScoped<GetSaleItemHandler>()
                .AddScoped<GetCartHandler>()
                .AddScoped<CreateOrderHandler>()
                .AddScoped<AddCartLineHandler>();

            return services;
        }
    }
}