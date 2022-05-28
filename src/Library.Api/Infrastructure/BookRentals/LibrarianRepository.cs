namespace Library.Api.Infrastructure.BookRentals
{
    using System;
    using System.Threading.Tasks;
    using Domain.Users;

    public class LibrarianRepository : ILibrarianRepository, IDisposable
    {
        private readonly BookRentalDbContext _dbContext;

        public LibrarianRepository(BookRentalDbContext dbContext) => _dbContext = dbContext;

        public async ValueTask Add(Librarian entity) => await _dbContext.Librarians.AddAsync(entity);
        public async ValueTask<Librarian> Load(Guid id) => await _dbContext.Librarians.FindAsync(id);
        public async ValueTask<bool> Exists(Guid id) => await Load(id) != null;
        public async Task Commit() => await _dbContext.SaveChangesAsync();
        public void Dispose() => _dbContext.Dispose();
    }
}
