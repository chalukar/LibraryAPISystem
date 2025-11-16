using FluentAssertions;
using Library.Application.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.SystemTests
{
    public class BorrowReturnFlowTests : IClassFixture<SystemTestFactory>
    {
        private readonly HttpClient _client;
        private readonly IServiceProvider _services;

        public BorrowReturnFlowTests(SystemTestFactory factory)
        {
            _client = factory.CreateClient();
            _services = factory.Services;
        }

        [Fact]
        public async Task BorrowReturnFlow_ShouldWorkCorrectly()
        { 
            // Arrange
            // 1️ Create Book via HTTP API
            var createdBook = await (await _client.PostAsJsonAsync("/api/books", new
            {
                title = ".NET Guide - New Version ",
                author = "Philip",
                isbn = "01147855482",
                pages = 150,
                totalCopies = 3
            }))
            .Content.ReadFromJsonAsync<BookDto>();

            createdBook.Should().NotBeNull();

            // 2️ Create Borrower directly in the database (no HTTP endpoint exists)
            Guid borrowerId;
            
            using (var scope = _services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
                var borrower = new Borrower("Kamal", "kamal@mail.com");
                db.Borrowers.Add(borrower);
                await db.SaveChangesAsync();
                borrowerId = borrower.Id;
            }

            // 3️ Borrow book
            var borrowResp = await _client.PostAsync($"/api/lending/borrow?bookId={createdBook!.Id}&borrowerId={borrowerId}", null);

            borrowResp.StatusCode.Should().Be(HttpStatusCode.OK);


            // 4️ Validate available copies decreased
            var afterBorrow = await _client.GetFromJsonAsync<BookDto>($"/api/books/{createdBook.Id}");
            afterBorrow!.AvailableCopies.Should().Be(2);

           
        }
    }
}
