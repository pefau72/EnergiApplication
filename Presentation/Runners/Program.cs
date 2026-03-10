// This is the presentation layer of the application, responsible for setting up the console application,
// configuring services, and starting the main application logic through the Runner class.

using EnergiApp.Application.Utils;
using EnergiApp.Domain.Repositories;
using EnergiApp.Domain.Services;
using EnergiApp.Infrastructure.Persistence.db;
using EnergiApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

// Create a new application builder for a console application.
var builder = Host.CreateApplicationBuilder(args); // Initializes a new instance of the HostBuilder class, which is used to configure and build the application host.

// Henter sektioner fra appsettings.json og binder den til klasser.
builder.Services.Configure<NordPoolApiOptions>(builder.Configuration.GetSection("NordPoolApi")); 
builder.Services.Configure<NordPoolSsoOptions>(builder.Configuration.GetSection("NordPoolSso"));
builder.Services.AddDbContext<AppDbContext>(options =>  options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

// Repositories
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>(); //
builder.Services.AddScoped<ITradeRepository, TradeRepository>();
builder.Services.AddScoped<ICurveOrderRepository, CurveOrderRepository>();


// Register the Runner class as a singleton service in the dependency injection container. 
builder.Services.AddSingleton<Runner>();
builder.Services.AddSingleton<ICurveOrderService, CurveOrderService>();

// Build the application, and compile configuration into a runnable application instance. 
var app = builder.Build();

// Retrieve Runner instance and call its RunAsync method to start the application logic.
await app.Services.GetRequiredService<Runner>().RunAsync();
