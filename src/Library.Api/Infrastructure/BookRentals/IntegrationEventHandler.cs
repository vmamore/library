namespace Library.Api.Infrastructure.BookRentals
{
    using System.Threading.Tasks;
    using Library.Api.Domain.BookRentals;
    using Library.Api.Infrastructure.Integrations;
    using Microsoft.Extensions.DependencyInjection;

    public class IntegrationEventHandler
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public IntegrationEventHandler(IServiceScopeFactory scopeFactory) => _scopeFactory = scopeFactory;

        public async Task Handle(IntegrationEvents.BookRegistered bookRegistered)
        {
            await using var scope = _scopeFactory.CreateAsyncScope();

            var context = scope.ServiceProvider.GetRequiredService<BookRentalDbContext>();

            var book = Book.Create(bookRegistered.Title, bookRegistered.Author, bookRegistered.PhotoUrl);

            await context.Books.AddAsync(book);

            await context.SaveChangesAsync();
        }
    }
}
