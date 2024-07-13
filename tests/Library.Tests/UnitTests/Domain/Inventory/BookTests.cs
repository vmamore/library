using Library.Api.Domain.Inventory;
using static Library.Api.Domain.Inventory.Events.V1;

namespace Library.Tests.UnitTests.Domain.Inventory
{
    public class BookTests
    {
        public BookTests()
            => Book = Book.Create(
                "Fight Club",
                "Chuck Palahniuk",
                "1996",
                "ISBN",
                208,
                1,
                "https://photo");

        Book Book { get; }

        [Fact]
        public void Created_Book()
        {
            Book.Title.Should().Be("Fight Club");
            Book.Author.Should().Be("Chuck Palahniuk");
            Book.ReleasedYear.Should().Contain("1996");
            Book.Pages.Should().Be(208);
            Book.Version.Should().Be(1);
            var events = Book.GetChanges();
            events.Should().ContainSingle();
            events.First().Should().BeOfType<BookRegistered>();
        }
    }
}
