using FluentAssertions;
using Library.Application.Commands;
using Library.Application.Handlers.CommandHandlers;
using Library.Domain.Entities;
using Library.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.UnitTests.Application.CommandHandlersTests
{
    public class ReturnBookHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldMarkReturned_AndIncreaseCopies()
        {
            var book = new Book("Pro C# 10", "Andrew Troelsen, Philip Japikse", "9781484278680", 1353, 5);
            var borrower = new Borrower("Chaluka", "Rathnayaka@mail.com");

            // Act
            var record = new LendingRecord(book, borrower);
            book.Borrow();

            var lendingRepo = new Mock<ILendingRecordRepository>();
            var bookRepo = new Mock<IBookRepository>();

            lendingRepo.Setup(r => r.GetByIdAsync(record.Id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(record);

            bookRepo.Setup(r => r.GetByIdAsync(record.BookId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(book);

            var handler = new ReturnBookHandler(lendingRepo.Object, bookRepo.Object);
            var command = new ReturnBookCommand(record.Id);

            var result = await handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            book.AvailableCopies.Should().Be(5);
            record.ReturnedAt.Should().NotBeNull();
        }
    }
}
