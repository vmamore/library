namespace Library.Api.Domain.BookRentals
{
    using Library.Api.Domain.Core;
    using Library.Api.Domain.Shared.ValueObjects;
    using static Library.Api.Domain.BookRentals.Events.V1;

    public class Book : Entity
    {
        public enum BookStatus
        {
            Free = 1,
            Rented
        }

        private Book() { }
        public static Book Create(string title, string isbn)
        {
            var book = new Book();

            book.Apply(new BookRegistered
            {
                Title = title,
                ISBN = isbn
            });

            return book;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public BookStatus Status { get; private set; }
        public ISBN ISBN { get; private set; }

        public bool IsRented() => this.Status == BookStatus.Rented;

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case BookRegistered e:
                    this.Status = BookStatus.Free;
                    this.Title = e.Title;
                    this.ISBN = new ISBN(e.ISBN);
                    break;
                case RentalCreated:
                    this.Status = BookStatus.Rented;
                    break;
                case RentalReturned:
                    this.Status = BookStatus.Free;
                    break;
                default:
                    break;
            }
        }
    }
}
