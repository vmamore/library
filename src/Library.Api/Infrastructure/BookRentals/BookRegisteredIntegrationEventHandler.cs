namespace Library.Api.Infrastructure.BookRentals
{
    using System.Threading.Tasks;
    using Integrations.Events;
    using Library.Api.Domain.BookRentals;
    using Integrations;

    public sealed class BookRegisteredIntegrationEventHandler : IIntegrationEventHandler<BookRegistered>
    {
        private readonly BookRentalDbContext dbContext;
        public BookRegisteredIntegrationEventHandler(BookRentalDbContext dbContext) => this.dbContext = dbContext;

        public async Task HandleAsync(BookRegistered @event)
        {
            var book = Book.Create(@event.Title, @event.Author, @event.PhotoUrl);

            await this.dbContext.Books.AddAsync(book);

            await this.dbContext.SaveChangesAsync();
        }
    }
}
