namespace Library.Api.Domain.BookRentals
{
    using System;
    using System.Collections.Generic;
    using Library.Api.Domain.Core;
    using Library.Api.Domain.Users;
    using static Library.Api.Domain.BookRentals.Events.V1;

    public class BookRental : AggregateRoot
    {
        public Guid Id { get; private set; }
        public enum BookRentStatus
        {
            OnGoing = 1,
            Done,
            Late
        }

        public DateTime RentedDay { get; private set; }
        public BookReturnDate DayToReturn { get; private set; }
        public DateTime? ReturnedDay { get; private set; }

        public BookRentStatus Status { get; private set; }


        private List<Book> _books;
        public Guid LocatorId { get; private set; }
        private Locator _locator;
        public Guid LibrarianId { get; private set; }
        private Librarian _librarian;

        public IEnumerable<Book> Books => _books.AsReadOnly();

        private BookRental() { }

        private BookRental(IEnumerable<Book> books, Locator locator, Librarian librarian, BookReturnDate dayToReturnBook)
        {
            _books = books.ToList();
            _locator = locator;
            _librarian = librarian;
            DayToReturn = dayToReturnBook;

            Apply(new RentalCreated
            {
                RentedDay = DateTime.UtcNow,
                BooksId = _books.Select(b => b.Id).ToArray(),
                DayToReturn = dayToReturnBook,
                LibrarianId = librarian.Id,
                LocatorId = locator.Id
            });
        }

        public static async Task<BookRental> Create(Librarian librarian, Locator locator, IEnumerable<Book> books, DateTime dayToReturnBooks)
        {
            if (locator is null)
                throw new InvalidOperationException("Locator must be valid.");

            if (books.Any(c => c.IsRented()))
                throw new InvalidOperationException("Cannot rent a book that is rented.");

            return new BookRental(books, locator, librarian, BookReturnDate.Create(dayToReturnBooks));
        }

        public void Returned(Guid librarianId) => throw new NotImplementedException();

        public override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case RentalCreated e:
                    Id = Guid.NewGuid();
                    Status = BookRentStatus.OnGoing;
                    RentedDay = e.RentedDay;
                    LocatorId = e.LocatorId;
                    LibrarianId = e.LibrarianId;
                    DayToReturn = BookReturnDate.Create(e.DayToReturn);
                    break;
                case RentalReturned e:
                    Status = BookRentStatus.Done;
                    ReturnedDay = e.ReturnedDay;
                    break;
                default:
                    break;
            }
        }

        public override void EnsureValidState() { }
    }
}
