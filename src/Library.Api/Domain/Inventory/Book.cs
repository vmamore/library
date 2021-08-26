namespace Library.Api.Domain.Inventory
{
    using System;
    using Library.Api.Domain.Core;
    using Library.Api.Domain.Shared.ValueObjects;
    using static Library.Api.Domain.Inventory.Events.V1;

    public class Book : Entity
    {
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
        public ISBN ISBN { get; private set; }

        protected override void When(DomainEvent @event)
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
                    break;
                default:
                    break;
            }
        }
    }
}
