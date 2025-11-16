using FluentAssertions;
using Library.Application.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Net.Http.Json;


namespace Library.Tests.FunctionalTests
{
    public class BooksApiTests : IClassFixture<LibraryApiFactory>
    {
        private readonly HttpClient _client;

        public BooksApiTests(LibraryApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateBook_ShouldReturnBookDto()
        {
            var newBook = new 
            { 
                title = ".NET Guide", 
                author = "Philip", 
                isbn = "01147852", 
                pages = 150, 
                totalCopies = 3 
            };

            var response = await _client.PostAsJsonAsync("/api/books", newBook);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var dto = await response.Content.ReadFromJsonAsync<BookDto>();
            dto!.Title.Should().Be(".NET Guide");
        }

        [Fact]
        public async Task GetBooks_ShouldReturnOk()
        {
            var resp = await _client.GetAsync("/api/books");
            resp.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetBookById_ShouldReturnBook()
        {
            var create = await _client.PostAsJsonAsync("/api/books",new 
            { 
                title = ".NET Guide", 
                author = "Philip", 
                isbn = "145874521544", 
                pages = 100, 
                totalCopies = 1 
            });

            var dto = await create.Content.ReadFromJsonAsync<BookDto>();

            var resp = await _client.GetAsync($"/api/books/{dto!.Id}");
            resp.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateBook_ShouldReturnUpdatedBook()
        {
            var created = await _client.PostAsJsonAsync("/api/books", new
            {
                title = ".NET Guide",
                author = "Vimal",
                isbn = "21545784512",
                pages = 100,
                totalCopies = 1
            });

            var dto = await created.Content.ReadFromJsonAsync<BookDto>();

            var updateDto = new
            {
                id = dto!.Id,
                title = ".NET Guide(Updated)",
                author = "Kamal",
                isbn = "2254154872",
                pages = 150,
                totalCopies = 2
            };

            var resp = await _client.PutAsJsonAsync($"/api/books/{dto.Id}", updateDto);
            resp.StatusCode.Should().Be(HttpStatusCode.OK);

            var updated = await resp.Content.ReadFromJsonAsync<BookDto>();
            updated!.Title.Should().Be(".NET Guide(Updated)");
        }

        [Fact]
        public async Task DeleteBook_ShouldReturnNoContent()
        {
            var created = await _client.PostAsJsonAsync("/api/books", new
            {
                title = ".NET Guide(Updated)",
                author = "Kamal",
                isbn = "2254154872",
                pages = 100,
                totalCopies = 1
            });

            var dto = await created.Content.ReadFromJsonAsync<BookDto>();
 

            var resp = await _client.DeleteAsync($"/api/books/{dto!.Id}");
            resp.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task MostBorrowed_ShouldReturnOk()
        {
            var resp = await _client.GetAsync("/api/books/most-borrowed");
            resp.StatusCode.Should().Be(HttpStatusCode.OK);
        }


    }
}
