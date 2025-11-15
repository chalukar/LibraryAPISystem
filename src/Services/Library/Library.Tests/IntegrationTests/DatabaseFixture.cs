using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.IntegrationTests
{
    public class DatabaseFixture : IDisposable
    {
        public LibraryDbContext Db { get; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase("TestDb_" + Guid.NewGuid())
                .Options;

            Db = new LibraryDbContext(options);
            Db.Database.EnsureCreated();
        }
        public void Dispose()
        {
            Db.Database.EnsureDeleted();
            Db.Dispose();
        }
    }
}
