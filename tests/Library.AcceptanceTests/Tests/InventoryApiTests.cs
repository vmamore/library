using FluentAssertions;
using Library.AcceptanceTests.Configurations;
using Library.AcceptanceTests.Tests.Configurations;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Library.AcceptanceTests.Tests
{
    public class InventoryApiTests : AcceptanceTest
    {
        public InventoryApiTests(CustomWebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async Task PostBook_ShouldReturn_200()
        {
            // Act
            var content = CreatePayload(new { Title = "Fight Club", Author = "Chuck Palahniuk", ReleasedYear = "1996", ISBN = "12345678910", PhotoUrl = "www.photo.com/1", Pages = 208, Version = 1 });
            var response = await CreateBookInInventory(content);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData(null, "Chuck Palahniuk", "1996")]
        [InlineData("Fight Club", null, "1996")]
        [InlineData("Fight Club", "Chuck Palahniuk", null)]
        public async Task PostBookWithInvalidProperties_ShouldReturn_400(string title, string author, string releasedYear)
        {
            // Act
            var content = CreatePayload(new { Title = title, Author = author, ReleasedYear = releasedYear, Pages = 208, Version = 1 });
            var response = await CreateBookInInventory(content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
