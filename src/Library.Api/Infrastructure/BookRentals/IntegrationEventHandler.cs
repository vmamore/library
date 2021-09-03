namespace Library.Api.Infrastructure.BookRentals
{
    using Library.Api.Domain.BookRentals;
    using Library.Api.Infrastructure.Integrations;

    public class IntegrationEventHandler
    {
        private readonly BookRentalDbContext _context;

        public IntegrationEventHandler(BookRentalDbContext context) => _context = context;

        public async Task Handle(IntegrationEvents.BookRegistered bookRegistered)
        {
            var book = Book.Create(bookRegistered.Title, bookRegistered.Author);

            await _context.Books.AddAsync(book);

            await _context.SaveChangesAsync();
        }
    }
}
