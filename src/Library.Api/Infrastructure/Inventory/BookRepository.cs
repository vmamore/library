namespace Library.Api.Infrastructure.Inventory
{
    using System;
    using System.Threading.Tasks;
    using Library.Api.Domain.Inventory;

    public class BookRepository : IBookRepository, IDisposable
    {
        private readonly InventoryDbContext _dbContext;

        public BookRepository(InventoryDbContext dbContext) => _dbContext = dbContext;

        public async ValueTask Add(Book entity) => await _dbContext.Books.AddAsync(entity);
        public async ValueTask<bool> Exists(Guid id) => await Load(id) != null;
        public ValueTask<Book> Load(Guid id) => _dbContext.Books.FindAsync(id);

        public void Dispose() => _dbContext.Dispose();
    }
}
