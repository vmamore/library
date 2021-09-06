using FluentAssertions;
using Library.Api.Domain.BookRentals;
using Xunit;

namespace Library.UnitTests.BookRentals
{
    public class BookTests
    {
        public BookTests()
            => Book = Book.Create(
                "Fight Club",
                "Chuck Palahniuk");

        Book Book { get; }

        [Fact]
        public void Created_Book()
        {
            Book.Title.Should().Be("Fight Club");
            Book.Author.Should().Be("Chuck Palahniuk");
        }
    }
}
