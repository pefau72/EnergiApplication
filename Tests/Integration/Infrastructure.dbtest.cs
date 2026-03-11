using EnergiApp.Infrastructure.Persistence.db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Threading.Tasks;
using Xunit;
using EnergiApp.Presentation;

public class DatabaseConnectionTest
{
    [Fact]
    public async Task CanConnectToDatabase()
    {
        var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddUserSecrets(typeof(EnergiApp.Presentation.Program).Assembly)
        .Build();

        


        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(config.GetConnectionString("Postgres"))
            .Options;

        var conn = config.GetConnectionString("Postgres");
        Console.WriteLine(conn);

        await using var db = new AppDbContext(options);

        var canConnect = await db.Database.CanConnectAsync();

        Assert.True(canConnect);
    }

}

