namespace Library.Api.Infrastructure.BookRentals
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Library.Api.Domain.BookRentals;
    using Microsoft.EntityFrameworkCore;

    public class BookRentalRepository : IBookRentalRepository, IDisposable
    {
        private readonly BookRentalDbContext _dbContext;

        public BookRentalRepository(BookRentalDbContext dbContext) => _dbContext = dbContext;

        public async ValueTask Add(BookRental entity) => _dbContext.BookRentals.Add(entity);
        public async ValueTask<bool> Exists(Guid id) => await Load(id) != null;
        public ValueTask<BookRental> Load(Guid id) => _dbContext.BookRentals.FindAsync(id);

        public async Task<IEnumerable<Book>> LoadBooks(Guid[] booksId) => await _dbContext.Books.Where(b => booksId.Contains(b.Id)).ToListAsync();
        public void Dispose() => _dbContext.Dispose();

    }
}
