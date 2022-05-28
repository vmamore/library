namespace Library.Api.Domain.BookRentals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Shared;
    using Shared.Core;
    using Users;
    using static Events.V1;

    public class BookRental : AggregateRoot
    {
        private const int DEFAULT_PENALTY_DAYS = 7;

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

        public Locator Locator
        {
            get; private set;
        }

        public Librarian Librarian
        {
            get; private set;
        }

        public IEnumerable<Book> Books => _books.AsReadOnly();

        private BookRental() { }

        private BookRental(IEnumerable<Book> books, Locator locator, BookReturnDate dayToReturnBook)
        {
            _books = books.ToList();
            Locator = locator;
            DayToReturn = dayToReturnBook;

            Apply(new RentalCreated
            {
                RentedDay = DateTime.UtcNow,
                BooksId = _books.Select(b => b.Id).ToArray(),
                DayToReturn = dayToReturnBook
            });
        }

        public static BookRental Create(Locator locator, IEnumerable<Book> books, DateTime dayToReturnBooks)
        {
            if (locator is null)
                throw new InvalidOperationException("Locator must be valid.");

            if (books == null || books.Count() == 0)
                throw new ArgumentException("Cannot create rental with empty books.");

            if (books.Any(c => c.IsRented()))
                throw new InvalidOperationException("Cannot rent a book that is rented.");

            return new BookRental(books, locator, BookReturnDate.Create(dayToReturnBooks));
        }

        public void Returned(ISystemClock clock)
        {
            if (clock.UtcNow > DayToReturn)
            {
                this.Locator.Apply(new LocatorPenalized
                {
                    PenalizedDate = clock.UtcNow,
                    PenaltyEnd = clock.UtcNow.AddDays(DEFAULT_PENALTY_DAYS),
                    Reason = $"Late return. Expected: {DayToReturn.Value.Date} Day Returned: {clock.UtcNow.Date}"
                });
            }
            Apply(new RentalReturned
            {
                ReturnedDay = clock.UtcNow
            });
        }

        public override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case RentalCreated e:
                    Id = Guid.NewGuid();
                    Status = BookRentStatus.OnGoing;
                    RentedDay = e.RentedDay;
                    DayToReturn = BookReturnDate.Create(e.DayToReturn);
                    ApplyToEntity(_books, e);
                    break;
                case RentalReturned e:
                    Status = BookRentStatus.Done;
                    ReturnedDay = e.ReturnedDay;
                    ApplyToEntity(_books, e);
                    ApplyToEntity(Locator, e);
                    break;
            }
        }

        public override void EnsureValidState() { }
    }
}
