using FluentAssertions;
using Library.Api.Domain.BookRentals;
using Library.Api.Domain.Shared;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static Library.Api.Domain.BookRentals.Book;
using static Library.Api.Domain.BookRentals.BookRental;
using static Library.Api.Domain.BookRentals.Events.V1;

namespace Library.UnitTests.Domain.BookRentals
{
    public class BookRentalTests
    {
        Mock<ISystemClock> _systemClock;

        IEnumerable<Book> Books { get; }

        public BookRentalTests()
        {
            Books = new List<Book>()
            {
                Book.Create("Fight Club", "Chuck Palahniuk", "https://photo"),
                Book.Create("Fight Club", "Chuck Palahniuk", "https://photo"),
                Book.Create("Fight Club", "Chuck Palahniuk", "https://photo"),
            };
            _systemClock = new();
        }

        Locator Locator { get; } = Locator.Create("Vinicius", "Mamoré", new DateTime(1997, 10, 25),
            "00011122233", "Campo Grande", "MS", "Rua Hehehe", "80");


        [Fact]
        public void Rented_Book()
        {
            var bookRental = BookRental.Create(Locator, Books, DateTime.UtcNow.AddDays(14));

            bookRental.Status.Should().Be(BookRentStatus.OnGoing);
            bookRental.Books.Should().HaveCount(Books.Count());
            var rentedEvent = bookRental.GetChanges().Last();
            rentedEvent.Should().BeOfType<RentalCreated>();
            bookRental.Books.All(b => b.Status == Book.BookStatus.Rented).Should().BeTrue();
        }

        [Fact]
        public void Returned_Book()
        {
            _systemClock.Setup(c => c.UtcNow)
                .Returns(DateTime.UtcNow);

            var bookRental = BookRental.Create(Locator, Books, DateTime.UtcNow.AddDays(14));

            bookRental.Status.Should().Be(BookRentStatus.OnGoing);
            bookRental.Returned(_systemClock.Object);
            var rentalReturnedEvent = bookRental.GetChanges().Last();
            rentalReturnedEvent.Should().BeOfType<RentalReturned>();
            bookRental.Status.Should().Be(BookRentStatus.Done);
        }

        [Fact]
        public void LateBookRentalReturn_ShouldPenalyzeLocator()
        {
            _systemClock.Setup(c => c.UtcNow).Returns(DateTime.UtcNow.AddDays(3));

            var bookRental = BookRental.Create(Locator, Books, DateTime.UtcNow.AddDays(1));

            bookRental.Status.Should().Be(BookRentStatus.OnGoing);
            bookRental.Returned(_systemClock.Object);
            var rentalReturnedEvent = bookRental.GetChanges().Last();
            rentalReturnedEvent.Should().BeOfType<RentalReturned>();
            bookRental.Status.Should().Be(BookRentStatus.Done);
            bookRental.Locator.IsPenalized(_systemClock.Object).Should().BeTrue();
        }

        [Fact]
        public void BookRentalWithoutLocator_ShouldNotBeCreated()
        {
            Action action = () => BookRental.Create(null, Books, DateTime.UtcNow.AddDays(14));

            action.Should().Throw<InvalidOperationException>()
                .WithMessage("Locator must be valid.");
        }

        [Theory]
        [MemberData(nameof(InvalidBooksData))]
        public void BookRentalWithoutBooks_ShouldNotBeCreated(List<Book> books)
        {
            Action action = () => BookRental.Create(Locator, books, DateTime.UtcNow.AddDays(14));

            action.Should().Throw<ArgumentException>()
                .WithMessage("Cannot create rental with empty books.");
        }

        [Fact]
        public void BookRentalWithRentedBook_ShouldNotBeCreated()
        {
            var book = Books.First();
            book.Set("Status", BookStatus.Rented);
            Action action = () => BookRental.Create(Locator, Books, DateTime.UtcNow.AddDays(14));

            action.Should().Throw<InvalidOperationException>()
                .WithMessage("Cannot rent a book that is rented.");
        }

        public static List<object[]> InvalidBooksData()
            => new List<object[]>
            {
                new object[] { null },
                new object[] { new List<Book>() }
            };

    }
}
