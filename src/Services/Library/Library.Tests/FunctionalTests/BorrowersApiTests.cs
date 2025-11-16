using FluentAssertions;
using Library.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.FunctionalTests
{
    public class BorrowersApiTests : IClassFixture<LibraryApiFactory>
    {
        private readonly HttpClient _client;
        public BorrowersApiTests(LibraryApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetTopBorrowers_ShouldReturnOk()
        {
            var response = await _client.GetAsync("/api/borrowers/top?from=2025-11-14&to=2025-11-16&top=10");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetReadingPace_ShouldReturnOkOrNotFound()
        {
            var borrowerId = Guid.NewGuid();

            var response = await _client.GetAsync($"/api/borrowers/{borrowerId}/reading-pace");

            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }
    }
}
