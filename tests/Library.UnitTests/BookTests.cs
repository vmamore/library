namespace Library.UnitTests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Library.Api.Books;
    using Xunit;
    using static Library.Api.Books.Book;
    using static Library.Api.Books.Events.V1;

    public class BookTests
    {
        public BookTests()
            => Book = Book.Create(
                "Fight Club",
                "Chuck Palahniuk",
                "1996",
                208,
                1);

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

        [Fact]
        public void Rented_Book()
        {
            var personid = Guid.NewGuid();
            Func<Task<DateTime>> dayToReturn = () => Task.FromResult(DateTime.UtcNow.AddDays(5));

            Book.Rent(personid, dayToReturn);

            Book.Status.Should().Be(BookStatus.Rented);
            Book.Rents.Should().ContainSingle();
            var rentedEvent = Book.GetChanges().Last();
            rentedEvent.Should().BeOfType<BookRented>();
        }

        [Fact]
        public void Returned_Book()
        {
            var personid = Guid.NewGuid();
            Func<Task<DateTime>> dayToReturn = () => Task.FromResult(DateTime.UtcNow.AddDays(5));
            Book.Rent(personid, dayToReturn);
            Book.Returned("It's fine");
            Book.Status.Should().Be(BookStatus.Available);
        }
    }
}
