namespace Library.Api.Infrastructure.BookRentals
{
    using System;
    using System.Threading.Tasks;
    using Library.Api.Domain.BookRentals;
    using Microsoft.EntityFrameworkCore;

    public class LocatorRepository : ILocatorRepository
    {
        private readonly BookRentalDbContext _dbContext;

        public LocatorRepository(BookRentalDbContext dbContext) => _dbContext = dbContext;

        public async ValueTask<Locator> Load(Guid id) => await _dbContext.Locators
            .Include(l => l.Penalties)
            .FirstOrDefaultAsync(l => l.Id == id);
        public async ValueTask Add(Locator entity) => await _dbContext.Locators.AddAsync(entity);
        public async ValueTask<bool> Exists(Guid id) => await Load(id) != null;
        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
