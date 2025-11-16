using Library.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Respawn;
using Respawn.Graph;

namespace Library.Tests.IntegrationTests
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private readonly string _connectionString;

        public LibraryDbContext Db { get; private set; } = default!;
        public Respawner Respawner { get; private set; } = default!;

        public DatabaseFixture()
        {
            // Load configuration from appsettings.Testing.json
            var config = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("appsettings.Testing.json", optional: false)
               .AddEnvironmentVariables()
               .Build();

            _connectionString = config.GetConnectionString("IntegrationTestDb")
                ?? throw new InvalidOperationException("Missing IntegrationTestDb connection string.");
        }

        public async Task InitializeAsync()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseSqlServer(_connectionString)
                .Options;

            Db = new LibraryDbContext(options);

            await Db.Database.MigrateAsync();

            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            Respawner = await Respawner.CreateAsync(conn, new RespawnerOptions
            {
                TablesToIgnore = new[] { new Table("__EFMigrationsHistory") }
            });
        }

        public async Task ResetAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            await Respawner.ResetAsync(conn);
        }

        public Task DisposeAsync()
        {
            Db.Dispose();
            return Task.CompletedTask;
        }
    }
}
