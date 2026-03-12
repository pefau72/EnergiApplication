using EnergiApp.Application;
using EnergiApp.Infrastructure;
using EnergiApp.Presentation.Runners;
namespace EnergiApp.Presentation;



public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddSingleton<Runner>();

        var app = builder.Build();

        await app.Services.GetRequiredService<Runner>().RunAsync();
    }
}
