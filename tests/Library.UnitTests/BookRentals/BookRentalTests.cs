using FluentAssertions;
using Library.Api.Domain.BookRentals;
using Library.Api.Domain.BookRentals.Users;
using Xunit;
using static Library.Api.Domain.BookRentals.BookRental;
using static Library.Api.Domain.BookRentals.Events.V1;

namespace Library.UnitTests.BookRentals
{
    public class BookRentalTests
    {
        IEnumerable<Book> Books { get; }

        public BookRentalTests()
            => Books = new List<Book>()
            {
                Book.Create("Fight Club", "Chuck Palahniuk"),
                Book.Create("Fight Club", "Chuck Palahniuk"),
                Book.Create("Fight Club", "Chuck Palahniuk"),
            };

        Librarian Librarian { get; } = Librarian.Create("Vinicius", "Mamoré", new DateTime(1997, 10, 25),
            "0001234567", "Campo Grande", "MS", "Rua Hehehe", "80");

        Locator Locator { get; } = Locator.Create("Vinicius", "Mamoré", new DateTime(1997, 10, 25),
            "0001234567", "Campo Grande", "MS", "Rua Hehehe", "80");


        [Fact]
        public void Rented_Book()
        {
            var bookRental = BookRental.Create(Librarian, Locator, Books, DateTime.UtcNow.AddDays(14));

            bookRental.Status.Should().Be(BookRentStatus.OnGoing);
            bookRental.Books.Should().HaveCount(Books.Count());
            var rentedEvent = bookRental.GetChanges().Last();
            rentedEvent.Should().BeOfType<RentalCreated>();
        }

        //[Fact]
        //public void Returned_Book()
        //{
        //    var librarianId = Guid.NewGuid();
        //    var personId = Guid.NewGuid();
        //    Func<Task<DateTime>> dayToReturn = () => Task.FromResult(DateTime.UtcNow.AddDays(5));
        //    Book.Rent(personId, librarianId, dayToReturn);
        //    Book.Returned(librarianId, "It's fine");
        //    Book.Status.Should().Be(BookStatus.Available);
        //}
    }
}
