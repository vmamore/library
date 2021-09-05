namespace Library.Api.Infrastructure.BookRentals
{
    using Library.Api.Domain.BookRentals;
    using Library.Api.Domain.BookRentals.Users;
    using Microsoft.EntityFrameworkCore;

    public class BookRentalDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookRental> BookRentals { get; set; }
        public DbSet<Locator> Locators { get; set; }
        public DbSet<Librarian> Librarians { get; set; }

        public BookRentalDbContext(DbContextOptions<BookRentalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookRental).Assembly);
        }
    }
}
