// This is the presentation layer of the application, responsible for setting up the console application,
// configuring services, and starting the main application logic through the Runner class.

using Microsoft.EntityFrameworkCore;
using EnergiApp.Domain.Repositories;
using EnergiApp.Infrastructure.Persistence.db;
using EnergiApp.Infrastructure.Persistence.Repositories;

using EnergiApp.Application.Utils;

// Create a new application builder for a console application.
var builder = Host.CreateApplicationBuilder(args);

//Bind the "NordpoolApi" section of the configuration to the NordpoolApiOptions class.
builder.Services.Configure<NordPoolApiOptions>(builder.Configuration.GetSection("NordPoolApi")); // Henter sektion fra appsettings.json og binder den til NordPoolApiOptions klassen, så vi kan bruge den i vores applikation.
builder.Services.Configure<NordPoolSsoOptions>(builder.Configuration.GetSection("NordPoolSso"));
builder.Services.AddDbContext<AppDbContext>(options =>  options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

// Repositories
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();
builder.Services.AddScoped<ICurveOrderRepository, CurveOrderRepository>();


// Register the Runner class as a singleton service in the dependency injection container. 
builder.Services.AddSingleton<Runner>();

// Build the application, and compile configuration into a runnable application instance. 
var app = builder.Build();

// Retrieve Runner instance and call its RunAsync method to start the application logic.
await app.Services.GetRequiredService<Runner>().RunAsync();
