namespace Library.Api.Application.Rentals
{
    using System.Threading.Tasks;
    using Infrastructure.BookRentals;
    using Infrastructure.Integrations;
    using Infrastructure.Integrations.Events;
    using Domain.BookRentals;

    public sealed class BookRegisteredIntegrationEventHandler(BookRentalDbContext dbContext)
        : IIntegrationEventHandler<BookRegistered>
    {
        public async Task HandleAsync(BookRegistered @event)
        {
            var book = Book.Create(@event.Title, @event.Author, @event.PhotoUrl);

            await dbContext.Books.AddAsync(book);

            await dbContext.SaveChangesAsync();
        }
    }
}
