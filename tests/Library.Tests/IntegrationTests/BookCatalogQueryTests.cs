using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Library.Api.Application.Rentals;
using Library.Api.Infrastructure.BookRentals;
using Npgsql;
using Xunit;

namespace Library.Tests.IntegrationTests
{
    public class BookCatalogQueryTests
    {
        private readonly NpgsqlConnection _connection;

        public BookCatalogQueryTests()
        {
            _connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=library_db;User Id=postgres;Password=post_pwd123;Include Error Detail=true;");
        }

        [Fact]
        public async Task When_FetchingBookCatalog_Should_ReturnBeAsExpected()
        {
            var expectedBookCatalog = new List<ReadModels.GetAllBooksPaginated.BookListItem>()
            {
                new()
                {
                    Id = Guid.Parse("44bf17e5-ffd2-4219-8211-28a37448a119"),
                    Title = "Fight Club",
                    Author = "Chuck Palahniuk",
                    Status = "1",
                    PhotoUrl = "https://m.media-amazon.com/images/P/0393327345.01._SCLZZZZZZZ_SX500_.jpg"
                },
                new()
                {
                    Id = Guid.Parse("bb58cd57-2148-4d46-a001-8cc4ce93ad4f"),
                    Title = "The Almanack of Naval Ravikant",
                    Author = "Eric Jorgenson",
                    Status = "1",
                    PhotoUrl = "https://m.media-amazon.com/images/P/B08FF8MTM6.01._SCLZZZZZZZ_SX500_.jpg"
                },
                new()
                {
                    Id = Guid.Parse("59ba2440-1f66-4ac7-8637-f68df6e1758d"),
                    Title = "Choke",
                    Author = "Chuck Palahniuk",
                    Status = "1",
                    PhotoUrl = "https://m.media-amazon.com/images/P/0385720920.01._SCLZZZZZZZ_SX500_.jpg"
                },
                new()
                {
                    Id = Guid.Parse("ef0e074f-2789-4635-8668-b6855d4b3973"),
                    Title = "Greenlights",
                    Author = "Matthew McConaughey",
                    Status = "1",
                    PhotoUrl = "https://m.media-amazon.com/images/I/51DZeZw7K0L.jpg"
                },
            };
            var result = await _connection.Query(new QueryModels.GetAllBooks());
            result.TotalCount.Should().Be(4);
            result.Books.Should().BeEquivalentTo(expectedBookCatalog);
        }
    }
}