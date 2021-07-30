namespace Library.Api.Books
{
    using System;
    using System.Threading.Tasks;
    using Library.Api.Infrastructure;

    public class BookRepository : IBookRepository, IDisposable
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext)
            => _dbContext = dbContext;

        public async ValueTask Add(Book book) => await _dbContext.Books.AddAsync(book);

        public async ValueTask<bool> Exists(Guid id)
            => await _dbContext.Books.FindAsync(id) != null;

        public ValueTask<Book> Load(Guid id)
            => _dbContext.Books.FindAsync(id);

        public void Dispose() => _dbContext.Dispose();
    }
}
