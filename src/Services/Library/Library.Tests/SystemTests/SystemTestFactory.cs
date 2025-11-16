using Grpc.Net.Client;
using Library.gRPC;
using Library.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Tests.SystemTests
{
    public class SystemTestFactory : WebApplicationFactory<Program>
    {
        public HttpClient HttpClient { get; private set; } = default!;
        public LibraryService.LibraryServiceClient GrpcClient { get; private set; } = default!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                // Remove SQL Server DbContextOptions
                var descriptor = services.FirstOrDefault(
                    s => s.ServiceType == typeof(DbContextOptions<LibraryDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                // Add InMemory EF Core for functional tests
                services.AddDbContext<LibraryDbContext>(options =>
                    options.UseInMemoryDatabase("SystemTestDB"));
            });
        }

        protected override void ConfigureClient(HttpClient client)
        {
            client.BaseAddress ??= new Uri("http://localhost");

            HttpClient = client;

            // Create gRPC channel from same server
            var channel = GrpcChannel.ForAddress(client.BaseAddress!, new GrpcChannelOptions
            {
                HttpClient = client
            });

            GrpcClient = new LibraryService.LibraryServiceClient(channel);
        }
    }
}
