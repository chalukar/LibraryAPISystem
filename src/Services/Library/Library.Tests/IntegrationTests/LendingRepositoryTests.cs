using FluentAssertions;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.IntegrationTests
{
    public class LendingRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        public LendingRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task AddLendingRecord_ShouldInsertIntoRealDatabase()
        {
            await _fixture.ResetAsync();

            var book = new Book("Pro C# 10", "Andrew Troelsen, Philip Japikse", "9781484278680", 1353, 5);
            var borrower = new Borrower("Chaluka", "chaluka@mail.com");
            var lending = new LendingRecord(book, borrower);

            await _fixture.Db.Books.AddAsync(book);
            await _fixture.Db.Borrowers.AddAsync(borrower);
            await _fixture.Db.LendingRecords.AddAsync(lending);
            await _fixture.Db.SaveChangesAsync();

            var count = await _fixture.Db.LendingRecords.CountAsync();
            count.Should().Be(1);
        }
    }
}
