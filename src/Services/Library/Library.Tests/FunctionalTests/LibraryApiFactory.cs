using Library.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;


namespace Library.Tests.FunctionalTests
{
    public class LibraryApiFactory : WebApplicationFactory<Program>
    {
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
                    options.UseInMemoryDatabase("FunctionalTestDb"));
            });
        }
    }
}
