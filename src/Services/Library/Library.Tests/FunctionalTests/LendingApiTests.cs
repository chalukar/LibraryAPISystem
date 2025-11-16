using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.FunctionalTests
{
    public class LendingApiTests : IClassFixture<LibraryApiFactory>
    {
        private readonly HttpClient _client;
        public LendingApiTests(LibraryApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]  
        public async Task LendBookBorrow_ShouldReturnOkOrBadRequest()
        {
            var lendRequest = new
            {
                bookId = Guid.NewGuid(),
                borrowerId = Guid.NewGuid(),
                lendDate = DateTime.UtcNow
            };
            var response = await _client.PostAsJsonAsync("/api/lending/borrow", lendRequest);
            response.StatusCode.Should().BeOneOf(System.Net.HttpStatusCode.OK, System.Net.HttpStatusCode.BadRequest);
        }
    }
}
