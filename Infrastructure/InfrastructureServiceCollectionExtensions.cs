namespace EnergiApp.Infrastructure;

using EnergiApp.Domain.Repositories;
using EnergiApp.Infrastructure.Persistence.db;
using EnergiApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("Postgres")));

        services.AddScoped<IAuctionRepository, AuctionRepository>();
        services.AddScoped<ITradeRepository, TradeRepository>();
        services.AddScoped<ICurveOrderRepository, CurveOrderRepository>();

        return services;
    }
}
