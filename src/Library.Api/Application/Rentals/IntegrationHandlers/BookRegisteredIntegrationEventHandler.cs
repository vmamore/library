namespace Library.Api.Application.Rentals
{
    using System.Threading.Tasks;
    using Infrastructure.BookRentals;
    using Infrastructure.Integrations;
    using Infrastructure.Integrations.Events;
    using Domain.BookRentals;

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
