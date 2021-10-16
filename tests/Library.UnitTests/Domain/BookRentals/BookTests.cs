using FluentAssertions;
using Library.Api.Domain.BookRentals;
using Xunit;

namespace Library.UnitTests.Domain.BookRentals
{
    public class BookTests
    {
        public BookTests()
            => Book = Book.Create(
                "Fight Club",
                "Chuck Palahniuk",
                "https://photo");

        Book Book { get; }

        [Fact]
        public void Create_Book_WithSuccess()
        {
            Book.Title.Should().Be("Fight Club");
            Book.Author.Should().Be("Chuck Palahniuk");
        }
    }
}
