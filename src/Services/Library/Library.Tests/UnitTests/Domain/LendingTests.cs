using FluentAssertions;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.UnitTests.Domain
{
    public class LendingTests
    {
        [Fact]
        public void LendingRecord_WhenCreated_ShouldSetBorrowedAt()
        {
            var book = new Book("Pro C# 10", "Andrew Troelsen, Philip Japikse", "9781484278680", 1353, 5);
            var borrower = new Borrower("Chaluka", "Rathnayaka@mail.com");

            // Act
            var rec = new LendingRecord(book, borrower);

            // Assert
            rec.Id.Should().NotBeEmpty();
            rec.BorrowedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
            rec.BookId.Should().Be(book.Id);
            rec.BorrowerId.Should().Be(borrower.Id);
            rec.ReturnedAt.Should().BeNull();
        }
    }
}
