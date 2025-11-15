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
    public class BorrowBookHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldBorrowBook_AndCreateLendingRecord()
        {
            var book = new Book("Pro C# 10", "Andrew Troelsen, Philip Japikse", "9781484278680", 1353, 5);

            var bookRepo = new Mock<IBookRepository>();
            var lendingRepo = new Mock<ILendingRecordRepository>();
            var borrowerRepo = new Mock<IBorrowerRepository>();

            bookRepo.Setup(r => r.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(book);

            // Create test borrower
            var borrower = new Borrower("Chaluka", "chaluka@mail.com");

            var borrowerId = Guid.NewGuid();

            var command = new BorrowBookCommand(book.Id, Guid.NewGuid());

            // Mock borrower returned from repo
            borrowerRepo.Setup(r => r.GetByIdAsync(command.BorrowerId, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(borrower);

            var handler = new BorrowBookHandler(bookRepo.Object, borrowerRepo.Object, lendingRepo.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            book.AvailableCopies.Should().Be(4);
            lendingRepo.Verify(r => r.AddAsync(It.IsAny<LendingRecord>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldFail_WhenBookNotFound()
        {
            var bookRepo = new Mock<IBookRepository>();
            var lendingRepo = new Mock<ILendingRecordRepository>();
            var borrowerRepo = new Mock<IBorrowerRepository>();

            bookRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Book?)null);

            var command = new BorrowBookCommand(Guid.NewGuid(), Guid.NewGuid());
            var handler = new BorrowBookHandler(bookRepo.Object, borrowerRepo.Object, lendingRepo.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
        }
    }
}
