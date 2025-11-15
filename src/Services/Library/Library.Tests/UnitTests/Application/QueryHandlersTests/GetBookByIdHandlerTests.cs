using AutoMapper;
using FluentAssertions;
using Library.Application.DTOs;
using Library.Application.Handlers.QueryHandlers;
using Library.Application.Queries;
using Library.Domain.Entities;
using Library.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.UnitTests.Application.QueryHandlersTests
{
    public class GetBookByIdHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnBook_WhenIdExists()
        {
            var book = new Book("Pro C# 10", "Andrew Troelsen, Philip Japikse", "9781484278680", 1353, 5);

            var repo = new Mock<IBookRepository>();
            var mapper = new Mock<IMapper>();

            repo.Setup(r => r.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            mapper.Setup(m => m.Map<BookDto>(book))
                  .Returns(new BookDto(book.Id, book.Title, book.Author, book.Isbn, book.Pages, book.TotalCopies, book.AvailableCopies));

            var handler = new GetBookByIdHandler(repo.Object, mapper.Object);

            var result = await handler.Handle(new GetBookByIdQuery(book.Id), CancellationToken.None);

            result.Should().NotBeNull();
            result.Title.Should().Be("Pro C# 10");
        }
    }
}
