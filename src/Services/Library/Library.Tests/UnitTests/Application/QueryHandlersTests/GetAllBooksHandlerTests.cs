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
    public class GetAllBooksHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnBookList()
        {
            var books = new List<Book>
            {
                new Book("Pro C# 10", "Andrew Troelsen, Philip Japikse", "9781484278680", 1353, 5)
            };

            var repo = new Mock<IBookRepository>();
            var mapper = new Mock<IMapper>();

            repo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(books);

            mapper.Setup(m => m.Map<IEnumerable<BookDto>>(books))
                  .Returns(books.Select(b => new BookDto(b.Id, b.Title, b.Author, b.Isbn, b.Pages, b.TotalCopies, b.AvailableCopies)));

            var handler = new GetAllBooksHandler(repo.Object, mapper.Object);

            var result = await handler.Handle(new GetAllBooksQuery(), CancellationToken.None);

            result.Should().HaveCount(1);
        }
    }
}
