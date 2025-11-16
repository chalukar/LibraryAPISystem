using FluentAssertions;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.IntegrationTests
{
    [Collection("Database collection")]
    public class BookRepositoryTests
    {
        private readonly DatabaseFixture _fixture;

        public BookRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        } 

        [Fact]
        public async Task Add_ShouldInsertBook()
        {
            // Ensure we start from a clean database state
            await _fixture.ResetAsync();

            var repo = new BookRepository(_fixture.Db);
            var book = new Book("Pro C# 10", "Andrew Troelsen, Philip Japikse", "9781484278680", 1353, 5);

            await repo.AddAsync(book, default);
            var count = await _fixture.Db.Books.CountAsync();
            count.Should().Be(1);

        }
    }
}
