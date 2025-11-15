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
    public class BorrowerRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;

        public BorrowerRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Add_ShouldInsertBorrower()
        {
            var repo = new BorrowerRepository(_fixture.Db);
            var borrower = new Borrower("Chaluka", "chaluka@mail.com");

            await repo.AddAsync(borrower, default);
            await _fixture.Db.SaveChangesAsync();

            (await _fixture.Db.Borrowers.CountAsync()).Should().Be(2);
        }

        [Fact]
        public async Task GetById_ShouldReturnBorrower()
        {
            var repo = new BorrowerRepository(_fixture.Db);
            var borrower = new Borrower("Rathnayaka", "Rathnayaka@mail.com");

            await repo.AddAsync(borrower, default);
            await _fixture.Db.SaveChangesAsync();

            var result = await repo.GetByIdAsync(borrower.Id, default);

            result.Should().NotBeNull();
            result!.Email.Should().Be("Rathnayaka@mail.com");
        }
    }
}
