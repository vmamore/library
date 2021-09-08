namespace Library.Api.Domain.BookRentals
{
    using Library.Api.Domain.Core;
    using static Library.Api.Domain.BookRentals.Events.V1;

    public class Book : Entity
    {
        public enum BookStatus
        {
            Free = 1,
            Rented
        }

        private Book() { }
        public static Book Create(string title, string author, string photoUrl)
        {
            var book = new Book();

            book.Apply(new BookRegistered
            {
                Title = title,
                Author = author,
                PhotoUrl = photoUrl
            });

            return book;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string PhotoUrl { get; private set; }
        public BookStatus Status { get; private set; }
        public IEnumerable<BookRental> Rentals { get; private set; }

        public bool IsRented() => this.Status == BookStatus.Rented;

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case BookRegistered e:
                    this.Id = Guid.NewGuid();
                    this.Status = BookStatus.Free;
                    this.Title = e.Title;
                    this.Author = e.Author;
                    this.PhotoUrl = e.PhotoUrl;
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
