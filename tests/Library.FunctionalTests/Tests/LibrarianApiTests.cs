using FluentAssertions;
using Library.AcceptanceTests.Configurations;
using Library.AcceptanceTests.Tests.Configurations;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Library.AcceptanceTests.Tests
{
    public class LibrarianApiTests : AcceptanceTest
    {
        public LibrarianApiTests(CustomWebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async Task PostLibrarian_ShouldReturn_200()
        {
            // Act
            var content = CreatePayload(new { FirstName = "Vinicius", LastName = "Mamoré", BirthDate = new DateTime(1997, 10, 25), CPF = "00011122233", Street = "Rua Alegria", City = "São Paulo", Number = "200", District = "São José" });
            var response = await CreateLibrarian(content);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData(null, "Mamoré", "00011122233")]
        [InlineData("Vinícius", null, "00011122233")]
        [InlineData("Vinícius", "Mamoré", null)]
        public async Task PostLibrarianWithInvalidProperties_ShouldReturn_400(string firstName, string lastName, string cpf)
        {
            // Act
            var content = CreatePayload(new { FirstName = firstName, LastName = lastName, BirthDate = new DateTime(1997, 10, 25), CPF = cpf, Street = "Rua Alegria", City = "São Paulo", Number = "200", District = "São José" });
            var response = await CreateLibrarian(content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
