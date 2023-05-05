using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PloomesAPI.Application;
using PloomesAPI.Application.Interfaces;
using PloomesAPI.Domain.Interfaces;
using PloomesAPI.Infra.Data.Context;
using PloomesAPI.Infra.Data.Repository;

namespace PloomesAPI.Infra.IoC
{
    public static class DependencyInjectionConfig
    {
        // Construtor com parametro de envio de configuração.
        public static void AddApplicationDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            AddApplicationDependencies(serviceCollection);
        }

        // Retorna as dependências dos objetos
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            // Context
            services.AddScoped<IDapperContext, DapperContext>();

            //Apps
            services.AddTransient<IProdutoApp, ProdutoApp>();

            //Repositories
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
        }
    }
}