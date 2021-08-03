namespace Library.Api.Domain.Books
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Library.Api.Domain.Core;
    using static Library.Api.Domain.Books.Events.V1;

    public class Book : AggregateRoot
    {
        public enum BookStatus
        {
            Available = 1,
            Rented
        }

        private Book() => this.Id = Guid.NewGuid();

        public static Book Create(string title, string author, string releasedYear, int pages, int version)
        {
            var book = new Book();

            book.Apply(new BookRegistered
            {
                Title = title,
                Author = author,
                ReleasedYear = releasedYear,
                Version = version,
                Pages = pages
            });

            return book;
        }
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ReleasedYear { get; private set; }
        public int Pages { get; private set; }
        public int Version { get; private set; }


        List<BookRent> _rents = new List<BookRent>();
        public IEnumerable<BookRent> Rents => _rents.AsEnumerable();
        BookRent _currentRent => _rents.FirstOrDefault(c => c.Status == BookRent.BookRentStatus.OnGoing);

        public BookStatus Status { get; private set; }

        public async Task Rent(Guid personId, Func<Task<DateTime>> getDayToReturnBook)
        {
            var dayToReturnBook = await getDayToReturnBook();

            Apply(new BookRented
            {
                BookId = this.Id,
                PersonId = personId,
                DayToReturn = dayToReturnBook,
                BookRentedId = Guid.NewGuid(),
                RentedDay = DateTime.UtcNow
            });
        }

        public void Returned(string bookCondition)
        {
            Apply(new BookReturned
            {
                BookCondition = bookCondition,
                BookId = this.Id,
                ReturnedDay = DateTime.UtcNow
            });
        }

        public override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case BookRegistered e:
                    this.Status = BookStatus.Available;
                    this.Title = e.Title;
                    this.Author = e.Author;
                    this.ReleasedYear = e.ReleasedYear;
                    this.Pages = e.Pages;
                    this.Version = e.Version;
                    _rents = new List<BookRent>();
                    break;
                case BookRented e:
                    this.Status = BookStatus.Rented;
                    var bookRent = new BookRent();
                    ApplyToEntity(bookRent, e);
                    _rents.Add(bookRent);
                    break;
                case BookReturned e:
                    this.Status = BookStatus.Available;
                    ApplyToEntity(_currentRent, e);
                    break;
                default:
                    break;
            }
        }

        public override void EnsureValidState()
        {
            var valid = (Status switch
            {
                BookStatus.Available =>
                    _currentRent == null,
                BookStatus.Rented =>
                    _currentRent != null,
                _ => true
            });

            if (!valid)
                throw new Exception("Invalid state");

        }
    }
}
