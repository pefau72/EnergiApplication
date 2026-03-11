// This is the presentation layer of the application, responsible for setting up the console application,
//
//
using EnergiApp.Application.Utils;
using EnergiApp.Domain.Repositories;
using EnergiApp.Domain.Services;
using EnergiApp.Infrastructure.Persistence.db;
using EnergiApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using static Runner;

namespace EnergiApp.Presentation;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.Configure<NordPoolApiOptions>(builder.Configuration.GetSection("NordPoolApi"));
        builder.Services.Configure<NordPoolSsoOptions>(builder.Configuration.GetSection("NordPoolSso"));
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

        builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
        builder.Services.AddScoped<ITradeRepository, TradeRepository>();
        builder.Services.AddScoped<ICurveOrderRepository, CurveOrderRepository>();

        builder.Services.AddSingleton<Runner>();
        builder.Services.AddSingleton<ICurveOrderService, CurveOrderService>();
        builder.Services.AddSingleton<IRefitClientFactory, RefitClientFactory>();

        var app = builder.Build();

        await app.Services.GetRequiredService<Runner>().RunAsync();
    }
}
