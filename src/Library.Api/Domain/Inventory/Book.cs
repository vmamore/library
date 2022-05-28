namespace Library.Api.Domain.Inventory
{
    using System;
    using Library.Api.Domain.Core;
    using Library.Api.Domain.Shared.ValueObjects;
    using static Library.Api.Domain.Inventory.Events.V1;

    public class Book : AggregateRoot
    {
        private Book() => this.Id = Guid.NewGuid();

        public static Book Create(string title, string author, string releasedYear, string isbn, int pages, int version, string photoUrl)
        {
            var book = new Book();

            book.Apply(new BookRegistered
            {
                Title = title,
                Author = author,
                ReleasedYear = releasedYear,
                Version = version,
                Pages = pages,
                ISBN = isbn,
                PhotoUrl = photoUrl
            });

            return book;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ReleasedYear { get; private set; }
        public string PhotoUrl { get; private set; }

        public int Pages { get; private set; }
        public int Version { get; private set; }
        public ISBN ISBN { get; private set; }

        public override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case BookRegistered e:
                    this.Title = e.Title;
                    this.Author = e.Author;
                    this.ReleasedYear = e.ReleasedYear;
                    this.Pages = e.Pages;
                    this.Version = e.Version;
                    this.ISBN = new ISBN(e.ISBN);
                    this.PhotoUrl = e.PhotoUrl;
                    break;
                default:
                    break;
            }
        }

        public override void EnsureValidState() { }
    }
}
