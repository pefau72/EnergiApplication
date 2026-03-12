namespace EnergiApp.Application;


using EnergiApp.Application.Utils;
using EnergiApp.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration config)
    {
        // Bind strongly typed options
        services.Configure<NordPoolApiOptions>(config.GetSection("NordPoolApi"));
        services.Configure<NordPoolSsoOptions>(config.GetSection("NordPoolSso"));

        var apiOptions = config.GetSection("NordPoolApi").Get<NordPoolApiOptions>()!;
        var ssoOptions = config.GetSection("NordPoolSso").Get<NordPoolSsoOptions>()!;

        // Refit clients
        services.AddRefitClient<INordPoolSsoClient>()
            .ConfigureHttpClient((sp, c) =>
            {
                var options = sp.GetRequiredService<IOptions<NordPoolSsoOptions>>().Value;
                c.BaseAddress = new Uri(options.BaseUrl);
            });

        services.AddRefitClient<INordPoolApiClient>()
            .ConfigureHttpClient((sp, c) =>
            {
                var options = sp.GetRequiredService<IOptions<NordPoolApiOptions>>().Value;
                c.BaseAddress = new Uri(options.BaseUrl);
            });



        // Domain services
        services.AddScoped<ICurveOrderService, CurveOrderService>();

        return services;
    }
}
