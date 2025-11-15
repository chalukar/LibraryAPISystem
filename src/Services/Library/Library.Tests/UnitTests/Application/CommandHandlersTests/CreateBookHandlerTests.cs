using AutoMapper;
using FluentAssertions;
using Library.Application.Commands;
using Library.Application.DTOs;
using Library.Application.Handlers.CommandHandlers;
using Library.Domain.Entities;
using Library.Domain.Repositories;
using Moq;


namespace Library.Tests.UnitTests.Application.CommandHandlersTests
{
    public class CreateBookHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateBook_AndReturnBookDto()
        {
            // Arrange
            var repo = new Mock<IBookRepository>();
            var mapper = new Mock<IMapper>();

            var dto = new CreateBookDto("Pro C# 10", "Andrew Troelsen, Philip Japikse", "9781484278680", 1353, 5);
            var command = new CreateBookCommand(dto);

            var book = new Book(dto.Title, dto.Author, dto.Isbn, dto.Pages, dto.TotalCopies);
            var bookDto = new BookDto(book.Id, book.Title, book.Author, book.Isbn, book.Pages, book.TotalCopies, book.AvailableCopies);

            mapper.Setup(m => m.Map<BookDto>(It.IsAny<Book>()))
                  .Returns(bookDto);

            repo.Setup(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var handler = new CreateBookHandler(repo.Object, mapper.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            repo.Verify(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Once);
            result.Value.Title.Should().Be("Pro C# 10");
        }
    }
}
