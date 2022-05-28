namespace Library.Api.Infrastructure.BookRentals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Library.Api.Domain.BookRentals;
    using Microsoft.EntityFrameworkCore;

    public class BookRentalRepository : IBookRentalRepository
    {
        private readonly BookRentalDbContext _dbContext;

        public BookRentalRepository(BookRentalDbContext dbContext) => _dbContext = dbContext;

        public async ValueTask Add(BookRental entity) => _dbContext.BookRentals.Add(entity);
        public async ValueTask<bool> Exists(Guid id) => await Load(id) != null;
        public async ValueTask<BookRental> Load(Guid id) => await _dbContext.BookRentals
            .Include(br => br.Books)
            .Include(br => br.Locator)
            .FirstOrDefaultAsync(br => br.Id == id);

        public async Task<IEnumerable<Book>> LoadBooks(Guid[] booksId) => await _dbContext.Books.Where(b => booksId.Contains(b.Id)).ToListAsync();
        public async ValueTask<BookRental> GetActive(Guid locatorId) =>
            await _dbContext.BookRentals
                .Where(br => br.Status != BookRental.BookRentStatus.Done)
                .FirstOrDefaultAsync(br => br.Locator.Id == locatorId);

        public async ValueTask Add(Book entity) => await _dbContext.Books.AddAsync(entity);
        public async Task Commit() => await _dbContext.SaveChangesAsync();

    }
}
