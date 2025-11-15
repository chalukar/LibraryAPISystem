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
    public class BookRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;

        public BookRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        } 

        [Fact]
        public async Task Add_ShouldInsertBook()
        {
            var repo = new BookRepository(_fixture.Db);
            var book = new Book("Pro C# 10", "Andrew Troelsen, Philip Japikse", "9781484278680", 1353, 5);

            await repo.AddAsync(book, default);
            await _fixture.Db.SaveChangesAsync();

            (await _fixture.Db.Books.CountAsync()).Should().Be(1);
        }
    }
}
