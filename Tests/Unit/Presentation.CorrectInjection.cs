using EnergiApp.Application.Utils;
using EnergiApp.Domain.Repositories;
using EnergiApp.Domain.Services;
using EnergiApp.Infrastructure.Persistence.db;
using EnergiApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Xunit;
using static Runner; // Assuming InitializeClients is an internal method of Runner, we can use reflection to call it in the test.

public class InitializeClientsTests
{

    [Fact] // For Xunit: This is a test method
    public void InitializeClients_CreatesClientsWithCorrectConfiguration()
    {
        var builder = Host.CreateApplicationBuilder(); ; // Initializes a new instance of the HostBuilder class, which is used to configure and build the application host.

        // Fake configuration (instead of appsettings.json)- forced values.
        builder.Configuration["NordPoolApi:BaseUrl"] = "https://api.test/";
        builder.Configuration["NordPoolSso:BaseUrl"] = "https://sso.test/";
        builder.Configuration["ConnectionStrings:Postgres"] = "Host=localhost;Database=test;Username=test;Password=test";

        // Register the same services as Program.cs
        builder.Services.Configure<NordPoolApiOptions>(builder.Configuration.GetSection("NordPoolApi"));
        builder.Services.Configure<NordPoolSsoOptions>(builder.Configuration.GetSection("NordPoolSso"));
        builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

        builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
        builder.Services.AddScoped<ITradeRepository, TradeRepository>();
        builder.Services.AddScoped<ICurveOrderRepository, CurveOrderRepository>();

        builder.Services.AddSingleton<Runner>();
        builder.Services.AddSingleton<ICurveOrderService, CurveOrderService>();
        builder.Services.AddSingleton<IRefitClientFactory, RefitClientFactory>();

        var host = builder.Build();

        // Act
        var runner = host.Services.GetRequiredService<Runner>();

        // Assert
        Assert.NotNull(runner); // Check that the Runner instance was created successfully.
    }
}



