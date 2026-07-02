using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderManagement.Application.Interfaces;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Persistence;
using OrderManagement.Infrastructure.Repositories;

namespace OrderManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            // Registra o DbContext utilizando SQL Server.
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));

                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
                options.LogTo(Console.WriteLine, LogLevel.Information);
            });

            // Repositórios
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IHistoricoStatusPedidoRepository, HistoricoStatusPedidoRepository>();

            // Services da camada Application
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IPedidoService, PedidoService>();

            return services;
        }
    }
}
