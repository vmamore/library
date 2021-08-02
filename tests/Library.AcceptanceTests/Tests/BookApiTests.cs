using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Library.AcceptanceTests.Tests
{
    public class BookApiTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public BookApiTests(CustomWebApplicationFactory<Startup> factory) => _factory = factory;

        [Fact]
        public async Task PostBook()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var payload = JsonSerializer.Serialize(new { Title = "Fight Club", Author = "Chuck Palahniuk", ReleasedYear = "1996", Pages = 208, Version = 1 });
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("books", content);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData(null, "Chuck Palahniuk", "1996")]
        [InlineData("Fight Club", null, "1996")]
        [InlineData("Fight Club", "Chuck Palahniuk", null)]
        public async Task PostBookWithInvalidProperties_ShouldReturn_400(string title, string author, string releasedYear)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var payload = JsonSerializer.Serialize(new { Title = title, Author = author, ReleasedYear = releasedYear, Pages = 208, Version = 1 });
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("books", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
