namespace Library.Api.Infrastructure
{
    using System.Threading.Tasks;
    using Library.Api.Core;

    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _dbContext;

        public EfCoreUnitOfWork(LibraryDbContext dbContext)
            => _dbContext = dbContext;

        public Task Commit() => _dbContext.SaveChangesAsync();
    }
}
