using EnergiApp.Infrastructure.Persistence.db;
using Xunit;
using Testcontainers.PostgreSql;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace Tests.Component
{    
    public class PostgresFixture : IAsyncLifetime
    {
        private readonly PostgreSqlContainer _container;

        public AppDbContext DbContext { get; private set; }

        public PostgresFixture()
        {
            _container = new PostgreSqlBuilder()
                .WithImage("postgres:16")
                .WithDatabase("energiapp_test")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .WithCleanUp(true)
                .Build();
        }

        public async Task InitializeAsync()
        {
            await _container.StartAsync();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(_container.GetConnectionString())
                .Options;

            DbContext = new AppDbContext(options);

            await DbContext.Database.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
           await _container.DisposeAsync();
        }
    }

}
